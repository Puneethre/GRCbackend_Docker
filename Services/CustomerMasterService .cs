using AutoMapper;
using GRCServices.Data;
using ConsoleApp1.Data;
using GRCServices.Dto_s;
using GRCServices.Interfaces;
using GRCServices.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql;
using System.Net;
using BCryptNet = BCrypt.Net.BCrypt;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using ConsoleApp1.Models;
using System.Text;

namespace GRCServices.Services
{
    public class CustomerMasterService : ICustomerMaster
    {
        private readonly IMapper _mapper;
        private readonly GRCDbContext _context;
        //private readonly ILogger<CustomerService> _logger; ILogger<CustomerService> logger
        private readonly IConfiguration _configuration;
        private delegate string DelgSendEmail(string sSubj, string sBody, string toAddresses);
        private readonly DelgSendEmail? EmailHandler = null;

        public CustomerMasterService(GRCDbContext context, IMapper mapper , IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            EmailHandler = EmailUtility.SendEmail;
        }

        public async Task<ServiceResponse<List<GetCustomerMasterDto>>> AddCustomer(AddCustomerMasterDto newCustomerMasterdto)
        {
            var svcResponse = new ServiceResponse<List<GetCustomerMasterDto>>();
            StringBuilder sb = new StringBuilder();
            using (var transaction = _context.Database.BeginTransaction())
            {
                string connectionString = null;
                int sysuserid = 0;
                int customerid = 0;

                try
                {
                    var count = _context.SysCustomerInfos.Where(c => c.ContactEmail == newCustomerMasterdto.ContactEmail && c.ContactName == newCustomerMasterdto.ContactName && c.CustomerName == newCustomerMasterdto.CustomerName).Count();
                    if (count != 0)
                    {
                        Exception e = new Exception("Customer Name ,ContactName ,Contact Email Was Already there..!");
                        throw new Exception("", e);
                    }

                    //-------Update Customer Main Table-------
                    SysCustomerInfo newCustomer = _mapper.Map<SysCustomerInfo>(newCustomerMasterdto);

                    svcResponse.Data = new List<GetCustomerMasterDto>();
                    svcResponse.Errors = Array.Empty<string>();

                    int nRecs = _context.SysCustomerInfos.Count();
                    if (nRecs > 0)
                        newCustomer.CustomerId = _context.SysCustomerInfos.Max(c => c.CustomerId) + 1;
                    else
                        newCustomer.CustomerId = 1;   //Start with 1.
                    customerid = newCustomer.CustomerId;
                    _context.SysCustomerInfos.Add(newCustomer);
                    await _context.SaveChangesAsync();

                    ///  Adding to SystemCustomerLink tbl
                    SysCustomerLink newCustomerLink = new SysCustomerLink();

                    int nCustomerLinksRecs = _context.SysCustomerLinks.Count();
                    if (nCustomerLinksRecs > 0)
                        newCustomerLink.Id = _context.SysCustomerLinks.Max(c => c.Id) + 1;
                    else
                        newCustomerLink.Id = 1;

                    newCustomerLink.CustomerId = newCustomer.CustomerId;
                    newCustomerLink.DbConStr = $"Server=192.168.29.128;Port=5432;Database={newCustomerMasterdto.CustomerName.Replace(" ", "").ToLower()};Username=GRC;Password=Welcome@0668;Include Error Detail=true";
                    connectionString = newCustomerLink.DbConStr;
                    newCustomerLink.CreatedBy = newCustomer.CreatedBy;
                    newCustomerLink.CreatedDateTime = DateTime.Now;
                    newCustomerLink.IsActive = newCustomer.IsActive;

                    _context.SysCustomerLinks.Add(newCustomerLink);
                    await _context.SaveChangesAsync();

                    var script = GenerateMigrationScript("Server=192.168.29.128;Port=5432;Database=grc_master;Username=GRC;Password=Welcome@0668;Include Error Detail=true");

                    script = script.Replace("CREATE DATABASE current_database", $"CREATE DATABASE {newCustomerMasterdto.CustomerName.Replace(" ", "").ToLower()}");

                    CreateDatabase(_context.Database.GetConnectionString(), newCustomerMasterdto.CustomerName.Replace(" ", "").ToLower());

                    ExecuteScript(script, newCustomerLink.DbConStr);


                    

                    string scriptfordata = File.ReadAllText(_configuration.GetSection(key: "SqlScriptFilePath:file").Value);

                    ExecuteScript(scriptfordata, newCustomerLink.DbConStr);

                    SysUserLogin newSysUser = new SysUserLogin();

                    ///   Updating User in SystemTables
                    int nsysRecs = _context.SysUserLogins.Count();
                    if (nsysRecs > 0)
                        newSysUser.SysUserId = _context.SysUserLogins.Max(c => c.SysUserId) + 1;
                    else
                        newSysUser.SysUserId = 1;

                    newSysUser.Name = newCustomerMasterdto.ContactName;
                    newSysUser.NewUser = true;
                    newSysUser.LoginEmailId = newCustomerMasterdto.ContactEmail;
                    newSysUser.PasswordHash = HashPassword("Welcome@123");
                    newSysUser.LoginAttemptsAllowed = 0;
                    newSysUser.IsActive = newCustomerMasterdto.IsActive;
                    newSysUser.CreatedBy = newCustomerMasterdto.CreatedBy;
                    newSysUser.SysRoleId = _context.SysRoleMasters.Where(m => m.RoleTypeId == 1).Select(m => m.SysRoleId).FirstOrDefault();
                    newSysUser.CreatedDatetime = DateTime.Now;
                    newSysUser.CustomerId = newCustomer.CustomerId;
                    newSysUser.NewUser = true;
                    newSysUser.Guid = Guid.NewGuid();

                    _context.SysUserLogins.Add(newSysUser);
                    await _context.SaveChangesAsync();

                    transaction.Commit();

                    /// Updating User to Client Database

                    var optionsBuilder = new DbContextOptionsBuilder<GRCDbMasterContext>();
                    optionsBuilder.UseNpgsql(connectionString);

                    using (var connection = new GRCDbMasterContext(optionsBuilder.Options))
                    {
                        using (var innerTransaction = connection.Database.BeginTransaction())
                        {
                            try
                            {
                                ConsoleApp1.Models.ClientUserInfo clientuser = new ConsoleApp1.Models.ClientUserInfo();
                                int nClientUserRecs = connection.ClientUserInfos.Count();
                                if (nClientUserRecs > 0)
                                    clientuser.CliUserId = connection.ClientUserInfos.Max(c => c.CliUserId) + 1;
                                else
                                    clientuser.CliUserId = 1;

                                clientuser.Name = newCustomerMasterdto.ContactName;
                                clientuser.Email = newCustomerMasterdto.ContactEmail;
                                clientuser.CustomerId = customerid;
                                clientuser.SysUserId = sysuserid;
                                clientuser.CliRoleId = connection.ClientRoleMasters.Where(m => m.RoleTypeId == 1).Select(m => m.CliRoleId).FirstOrDefault();
                                clientuser.IsActive = true;
                                clientuser.CreatedBy = newCustomerMasterdto.CreatedBy;

                                connection.ClientUserInfos.Add(clientuser);
                                await connection.SaveChangesAsync();

                                innerTransaction.Commit();

                                string resetPasswordUrl = $"{_configuration.GetSection(key: "resetlink:link").Value}{newSysUser.Guid}";

                                var Subject = _configuration.GetSection(key: "EmailBodyForpwdReset:Subject").Value;
                                var EmailBody = _configuration.GetSection(key: "EmailBodyForpwdReset:Body").Value;
                                string subject = string.Format($"{Subject}");
                                string strFooter = "<p> This is a system generated email. Please do not reply to this email and contact the IT support in case if you have any questions</p>";
                                string sBody = $"<html><body><img src='cid:logoImage' width='100%' height='80'></body>" +
                                        $"<h1>Dear {newCustomerMasterdto.ContactName}</h1>" + $" One Time Password : Welcome@123 \n" +
                                        $"{EmailBody}<br><a href='{resetPasswordUrl}'>{resetPasswordUrl}</a></html>";

                                sb.Append(sBody);
                                sb.Append(strFooter);
                                EmailHandler(subject, sb.ToString(), newCustomerMasterdto.ContactEmail);
                            }
                            catch (Exception ex)
                            {
                                innerTransaction.Rollback();
                                svcResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                                svcResponse.Message = "Error Occured While Adding Customer..";
                                svcResponse.Errors = new String[] { ex.InnerException.Message };
                            }
                        }
                    }

                    svcResponse.Message = "Customer Created Successfully...!";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    svcResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    svcResponse.Message = "Error Occured While Adding Customer..";
                    svcResponse.Errors = new String[] { ex.InnerException.Message };
                }
                finally
                {
                    transaction.Dispose();
                }
            }
            return svcResponse;
        }

        public async Task<ServiceResponse<List<GetCustomerMasterDto>>> GetAllCustomers()
        {
            var svcResponse = new ServiceResponse<List<GetCustomerMasterDto>>();
            try
            {
                List<GetCustomerMasterDto> returnlist = new List<GetCustomerMasterDto>();

                var custlist = _context.SysCustomerInfos
                    .Where(s => s.IsActive == true)
                      .OrderByDescending(c => c.CustomerId)
                      .ToList();

                foreach (var recs in custlist)
                {
                    GetCustomerMasterDto dto = new GetCustomerMasterDto();
                    dto.CustomerId = recs.CustomerId;
                    dto.CustomerName = recs.CustomerName;
                    dto.Street = recs.Street;
                    dto.Description = recs.Description;
                    dto.City = recs.City;
                    dto.State = recs.State;
                    dto.Country = recs.Country;
                    dto.ContactName = recs.ContactName;
                    dto.ContactPhone = recs.ContactPhone;
                    dto.ContactEmail = recs.ContactEmail;
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
            return svcResponse;
        }

        public async Task<ServiceResponse<List<GetCustomerMasterDto>>> UpdateCustomer(UpdateCustomerMasterDto updatedCustomer,int Customerid)
        {
            //Log.Information("Entering UpdateUserService", DateTime.UtcNow.ToLongTimeString());
            ServiceResponse<List<GetCustomerMasterDto>> svcResponse = new ServiceResponse<List<GetCustomerMasterDto>>();
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    svcResponse.Data = new List<GetCustomerMasterDto>();
                    svcResponse.Errors = new String[] { };

                    var tobeupdated = _context.SysCustomerInfos
                        .FirstOrDefault(c => c.CustomerId == Customerid);

                    if (tobeupdated != null)
                    {
                        // --- Update Address Table ---- 
                       //tobeupdated.CustomerName = updatedCustomer.CustomerName;
                        tobeupdated.Description = updatedCustomer.Description;
                        tobeupdated.Street = updatedCustomer.Street;
                        tobeupdated.State = updatedCustomer.State;
                        tobeupdated.Country = updatedCustomer.Country;
                        tobeupdated.ContactName = updatedCustomer.ContactName;
                        tobeupdated.ContactPhone = updatedCustomer.ContactPhone;
                        tobeupdated.ContactEmail = updatedCustomer.ContactEmail;
                        tobeupdated.Description = updatedCustomer.Description;

                        _context.SysCustomerInfos.Update(tobeupdated);
                        await _context.SaveChangesAsync();

                        svcResponse.Message = "Customer Updated Successfully...!";
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
                    //Log.Error("In Exception: UpdateCustomerService-", ex.Message);
                }
                finally
                {
                    transaction.Dispose();
                }
            }
            //Log.Information("Exiting UpdateCustomerService", DateTime.UtcNow.ToLongTimeString());
            return svcResponse;
        }

        public async Task<ServiceResponse<List<GetCustomerMasterDto>>> DeleteCustomer(int id)
        {
            ServiceResponse<List<GetCustomerMasterDto>> svcResponse = new ServiceResponse<List<GetCustomerMasterDto>>();
            try
            {
                SysCustomerInfo? tobedeletedcf = _context.SysCustomerInfos
                       .FirstOrDefault(c => c.CustomerId == id);

                svcResponse.Errors = Array.Empty<string>();
                svcResponse.Data = new List<GetCustomerMasterDto>();

                if (tobedeletedcf != null)
                {
                    //----Deleting in Queue Table--------

                    tobedeletedcf.IsActive = false;
                    _context.SysCustomerInfos.Update(tobedeletedcf);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    svcResponse.StatusCode = (int)HttpStatusCode.NotFound;
                    svcResponse.Message = "Record not found!";
                }

                svcResponse.Errors = Array.Empty<string>();
                svcResponse.Message = "Customer Deleted Successfully...!";
            }
            catch (Exception ex)
            {
                svcResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                svcResponse.Message = ex.Message;
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

        static void CreateDatabase(string serverConnectionString, string newDatabaseName)
        {
            try
            {
                // Connect to the PostgreSQL server (without specifying a database name)
                using (var connection = new NpgsqlConnection(serverConnectionString))
                {
                    connection.Open();

                    // Create the new database
                    using (var command = new NpgsqlCommand($"CREATE DATABASE {newDatabaseName}", connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    Console.WriteLine("New database created successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while creating the new database: " + ex.Message);
            }
        }

        static string GenerateMigrationScript(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<GRCDbMasterContext>();
            optionsBuilder.UseNpgsql(connectionString); // Specify your database provider here

            using (var dbContext = new GRCDbMasterContext(optionsBuilder.Options))
            {
                return dbContext.Database.GenerateCreateScript();
            }
        }

        static void ExecuteScript(string script, string connectionString)
        {
            try
            {
                // Connect to the PostgreSQL server
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Execute the modified script on the new database
                    using (var command = new NpgsqlCommand(script, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    Console.WriteLine("Schema replicated successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
