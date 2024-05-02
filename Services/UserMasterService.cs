using AutoMapper;
using ConsoleApp1.Data;
using GRCServices.Data;
using GRCServices.Dto_s;
using GRCServices.Interfaces;
using GRCServices.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Text;
using BCryptNet = BCrypt.Net.BCrypt;

namespace GRCServices.Services
{
    public class UserMasterService : IUserMaster
    {
        private readonly IMapper _mapper;
        private readonly GRCDbContext _context;
        private delegate string DelgSendEmail(string sSubj, string sBody, string toAddresses);
        private readonly DelgSendEmail? EmailHandler = null;
        private readonly IConfiguration _configuration;
        //private readonly ILogger<CustomerService> _logger; ILogger<CustomerService> logger
        public UserMasterService(GRCDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            EmailHandler = EmailUtility.SendEmail;
        }

        public async Task<ServiceResponse<List<GetUserMasterDto>>> AddUser(AddUserMasterDto newUserMasterdto)
        {
            var svcResponse = new ServiceResponse<List<GetUserMasterDto>>();
            StringBuilder sb = new StringBuilder();

            string connectionString = _context.SysCustomerLinks.Where(c => c.CustomerId == newUserMasterdto.CustomerId).Select(c => c.DbConStr).FirstOrDefault();

            var optionsBuilder = new DbContextOptionsBuilder<GRCDbMasterContext>();
            optionsBuilder.UseNpgsql(connectionString);

            using (var connection = new GRCDbMasterContext(optionsBuilder.Options))
            {
                using (var transaction = connection.Database.BeginTransaction())
                {
                    try
                    {
                        var count = _context.SysUserLogins.Where(c => c.LoginEmailId == newUserMasterdto.Email).Count();
                        if (count != 0)
                        {
                            Exception e = new Exception("Email Already There...!");
                            throw new Exception("", e);
                        }

                        SysUserLogin newSysUser = new SysUserLogin();

                        ///Updating User in SystemTables
                        int nsysRecs = _context.SysUserLogins.Count();
                        if (nsysRecs > 0)
                            newSysUser.SysUserId = _context.SysUserLogins.Max(c => c.SysUserId) + 1;
                        else
                            newSysUser.SysUserId = 1;

                       // newSysUser. = newUserMasterdto.Name;
                        newSysUser.NewUser = true;
                        newSysUser.LoginEmailId = newUserMasterdto.Email;
                       // newSysUser. = newUserMasterdto.PhoneNo;
                        newSysUser.PasswordHash = HashPassword("Welcome@123");
                        newSysUser.LoginAttemptsAllowed = 0;
                        newSysUser.IsActive = newUserMasterdto.Status;
                        newSysUser.SysRoleId = null;
                        newSysUser.CustomerId = newUserMasterdto.CustomerId;
                        newSysUser.CreatedBy = newUserMasterdto.CreatedBy;
                        newSysUser.CreatedDatetime = DateTime.Now;
                        newSysUser.Guid = Guid.NewGuid();

                        _context.SysUserLogins.Add(newSysUser);
                        await _context.SaveChangesAsync();

                        //-------Update Customer Main Table-------
                        ConsoleApp1.Models.ClientUserInfo newUser = _mapper.Map<ConsoleApp1.Models.ClientUserInfo>(newUserMasterdto);

                        svcResponse.Data = new List<GetUserMasterDto>();
                        svcResponse.Errors = Array.Empty<string>();

                        //Updating User in ClientTbls
                        int nRecs = connection.ClientUserInfos.Count();
                        if (nRecs > 0)
                            newUser.CliUserId = connection.ClientUserInfos.Max(c => c.CliUserId) + 1;
                        else
                            newUser.CliUserId = 1;   //Start with 1.

                        newUser.SysUserId = newSysUser.SysUserId;
                        connection.ClientUserInfos.Add(newUser);
                        await connection.SaveChangesAsync();

                        string resetPasswordUrl = $"http://localhost:3000/resetpassword?UID={newSysUser.Guid}";

                        var Subject = _configuration.GetSection(key: "EmailBodyForpwdReset:Subject").Value;
                        var EmailBody = _configuration.GetSection(key: "EmailBodyForpwdReset:Body").Value;
                        string subject = string.Format($"{Subject}");
                        string strFooter = "<p> This is a system generated email. Please do not reply to this email and contact the IT support in case if you have any questions</p>";
                        string sBody = $"<html><body><img src='cid:logoImage' width='100%' height='80'></body>" +
                                $"<h1>Dear {newUserMasterdto.Name}</h1>" + $" One Time Password : Welcome@123 \n" +
                                $"{EmailBody}<br><a href='{resetPasswordUrl}'>{resetPasswordUrl}</a></html>";

                        sb.Append(sBody);
                        sb.Append(strFooter);
                        EmailHandler(subject, sb.ToString(), newUserMasterdto.Email);

                        svcResponse.Message = "User Created Successfully...!";
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        svcResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                        svcResponse.Message = "Error Occured While Adding Role..";
                        svcResponse.Errors = new String[] { ex.InnerException.Message };
                    }
                    finally
                    {
                        transaction.Dispose();
                    }
                }
            }
            return svcResponse;
        }

        public async Task<ServiceResponse<List<GetUserMasterDto>>> GetAllUsers(int CustomerId)
        {
            var svcResponse = new ServiceResponse<List<GetUserMasterDto>>();
            string connectionString = _context.SysCustomerLinks.Where(c => c.CustomerId == CustomerId).Select(c => c.DbConStr).FirstOrDefault();

            var optionsBuilder = new DbContextOptionsBuilder<GRCDbMasterContext>();
            optionsBuilder.UseNpgsql(connectionString);

            using (var connection = new GRCDbMasterContext(optionsBuilder.Options))
            {
                try
                {
                    List<GetUserMasterDto> returnlist = new List<GetUserMasterDto>();

                    var Userlist = connection.ClientUserInfos
                                            .Where(c => c.IsActive == true)
                                            .OrderByDescending(c => c.CliUserId)
                                            .ToList();

                    foreach (var recs in Userlist)
                    {
                        GetUserMasterDto dto = new GetUserMasterDto();
                        dto.Id = recs.CliUserId;
                        dto.Name = recs.Name;
                        dto.Email = recs.Email;
                        dto.CustomerName = _context.SysCustomerInfos.Where(s => s.CustomerId == recs.CustomerId).Select(s => s.CustomerName).FirstOrDefault();
                        dto.Role = connection.ClientRoleMasters.Where(r => r.CliRoleId == recs.CliRoleId).Select(r => r.RoleName).FirstOrDefault();
                        dto.Status = recs.IsActive;
                        returnlist.Add(dto);
                    }
                    svcResponse.Errors = new string[] { };
                    svcResponse.Data = returnlist;
                }
                catch (Exception ex)
                {
                    svcResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    svcResponse.Errors = new string[] { ex.InnerException.Message };
                }
                connection.Dispose();
            }
            return svcResponse;
        }

        public async Task<ServiceResponse<List<GetUserMasterDto>>> UpdateUser(UpdateUserMasterDto updatedUser, int Userid)
        {
            ServiceResponse<List<GetUserMasterDto>> svcResponse = new ServiceResponse<List<GetUserMasterDto>>();
            string connectionString = _context.SysCustomerLinks.Where(c => c.CustomerId == updatedUser.CustomerId).Select(c => c.DbConStr).FirstOrDefault();

            var optionsBuilder = new DbContextOptionsBuilder<GRCDbMasterContext>();
            optionsBuilder.UseNpgsql(connectionString);

            using (var connection = new GRCDbMasterContext(optionsBuilder.Options))
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        svcResponse.Data = new List<GetUserMasterDto>();
                        svcResponse.Errors = new String[] { };

                        var tobeupdated = connection.ClientUserInfos
                            .FirstOrDefault(c => c.CliUserId == Userid);

                        var tobeupdatedsysusertbl = _context.SysUserLogins.FirstOrDefault(c => c.SysUserId == tobeupdated.SysUserId);

                        if (tobeupdated != null)
                        {
                            // --- Update System UserLogin Table ---- 
                            tobeupdatedsysusertbl.Name = updatedUser.Name;
                            tobeupdatedsysusertbl.IsActive = updatedUser.Status;

                            _context.SysUserLogins.Update(tobeupdatedsysusertbl);
                            await _context.SaveChangesAsync();

                            // --- Update Client User Table ---- 
                            tobeupdated.Name = updatedUser.Name;
                            tobeupdated.IsActive = updatedUser.Status;
                            tobeupdated.CliRoleId = updatedUser.CliRoleId;

                            connection.ClientUserInfos.Update(tobeupdated);
                            await connection.SaveChangesAsync();

                            svcResponse.Message = "User Updated Successfully...!";
                        }
                        else
                        {
                            svcResponse.Message = "You are Sent Null Values Please enter data";
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        svcResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                        svcResponse.Message = "Error Occured While Updating Customer...";
                        svcResponse.Errors = new string[] { ex.InnerException.Message };
                    }
                    finally
                    {
                        transaction.Dispose();
                    }
                }
            }
            return svcResponse;
        }

        public async Task<ServiceResponse<List<GetUserMasterDto>>> DeleteUser(int id,int CustomerId)
        {
            ServiceResponse<List<GetUserMasterDto>> svcResponse = new ServiceResponse<List<GetUserMasterDto>>();
            string connectionString = _context.SysCustomerLinks.Where(c => c.CustomerId == CustomerId).Select(c => c.DbConStr).FirstOrDefault();

            var optionsBuilder = new DbContextOptionsBuilder<GRCDbMasterContext>();
            optionsBuilder.UseNpgsql(connectionString);

            using (var connection = new GRCDbMasterContext(optionsBuilder.Options))
            {
                try
                {
                    ConsoleApp1.Models.ClientUserInfo? tobedeletedcf = connection.ClientUserInfos
                           .FirstOrDefault(c => c.CliUserId == id);

                    svcResponse.Errors = Array.Empty<string>();
                    svcResponse.Data = new List<GetUserMasterDto>();

                    if (tobedeletedcf != null)
                    {
                        //----Deleting in User Table--------
                        tobedeletedcf.IsActive = false;
                        connection.ClientUserInfos.Update(tobedeletedcf);
                        await connection.SaveChangesAsync();
                    }
                    else
                    {
                        svcResponse.StatusCode = (int)HttpStatusCode.NotFound;
                        svcResponse.Message = "Record not found!";
                    }

                    svcResponse.Errors = Array.Empty<string>();
                    svcResponse.Message = "User Deleted Successfully...!";
                }
                catch (Exception ex)
                {
                    svcResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    svcResponse.Message = ex.Message;
                }
            }
            return svcResponse;
        }

        public string HashPassword(string password)
        {
            // Generate a salt and hash the password using bcrypt
            string salt = BCryptNet.GenerateSalt();
            string hashedPassword = BCryptNet.HashPassword(password, salt);
            return hashedPassword;
        }
    }
}
