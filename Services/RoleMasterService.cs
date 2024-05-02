using AutoMapper;
using GRCServices.Data;
using GRCServices.Dto_s;
using GRCServices.Interfaces;
using GRCServices.Models;
//using ConsoleApp1.Models;
using System.Net;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Data.SqlClient;
using Npgsql;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using ConsoleApp1.Data;

namespace GRCServices.Services
{
    public class RoleMasterService : IRoleMaster
    {
        private readonly IMapper _mapper;
        private readonly GRCDbContext _context;
        //private readonly ILogger<CustomerService> _logger; ILogger<CustomerService> logger
        public RoleMasterService(GRCDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetRoleMasterDto>>> AddRole(AddRoleMasterDto newRoleMasterdto)
        {
            var svcResponse = new ServiceResponse<List<GetRoleMasterDto>>();

            string connectionString = _context.SysCustomerLinks.Where(c => c.CustomerId == newRoleMasterdto.CustomerId).Select(c => c.DbConStr).FirstOrDefault();

            var optionsBuilder = new DbContextOptionsBuilder<GRCDbMasterContext>();
            optionsBuilder.UseNpgsql(connectionString);

            using (var connection = new GRCDbMasterContext(optionsBuilder.Options))
            {
                using (var transaction = connection.Database.BeginTransaction())
                {
                    try
                    {
                        svcResponse.Data = new List<GetRoleMasterDto>();
                        svcResponse.Errors = Array.Empty<string>();

                        var count = connection.ClientRoleMasters.Where(c => c.RoleName == newRoleMasterdto.RoleName).Count();
                        if (count != 0)
                        {
                            Exception e = new Exception("Role Name Sholud be Unique");
                            throw new Exception("", e);
                        }
                        //-------Update Customer Main Table-------
                        ConsoleApp1.Models.ClientRoleMaster newRole = _mapper.Map<ConsoleApp1.Models.ClientRoleMaster>(newRoleMasterdto);

                        svcResponse.Data = new List<GetRoleMasterDto>();
                        svcResponse.Errors = Array.Empty<string>();

                        int nRecs = connection.ClientRoleMasters.Count();
                        if (nRecs > 0)
                            newRole.CliRoleId = connection.ClientRoleMasters.Max(c => c.CliRoleId) + 1;
                        else
                            newRole.CliRoleId = 1;   //Start with 1.

                        newRole.CreatedDateTime = DateTime.Now;
                        int? sysroleid = _context.SysUserLogins.Where(s => s.SysUserId == newRoleMasterdto.CreatedBy).Select(s => s.SysRoleId).FirstOrDefault();
                        int? roletype = connection.ClientRoleMasters.Where(s => s.CliRoleId == sysroleid).Select(s => s.RoleTypeId).FirstOrDefault();
                        if (roletype == 0)
                        {
                            newRole.RoleTypeId = 1;
                        }
                        else if (roletype == 1)
                        {
                            newRole.RoleTypeId = 2;
                        }
                        newRole.IsActive = newRoleMasterdto.Active;
                        connection.ClientRoleMasters.Add(newRole);
                        await connection.SaveChangesAsync();

                        svcResponse.Message = "Role Created Successfully...!";
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

        public async Task<ServiceResponse<List<GetRoleMasterDto>>> GetAllRoles(int CustomerId)
        {
            var svcResponse = new ServiceResponse<List<GetRoleMasterDto>>();
            try
            {
                string connectionString = _context.SysCustomerLinks.Where(c => c.CustomerId == CustomerId).Select(c => c.DbConStr).FirstOrDefault();

                var optionsBuilder = new DbContextOptionsBuilder<GRCDbMasterContext>();
                optionsBuilder.UseNpgsql(connectionString);
                using (var connection = new GRCDbMasterContext(optionsBuilder.Options))
                {
                    List<GetRoleMasterDto> returnlist = new List<GetRoleMasterDto>();
                    //&& c.RoleTypeId != 0
                    var custlist = connection.ClientRoleMasters
                          .Where(c => c.IsActive == true)
                          .OrderByDescending(c => c.CliRoleId)
                          .ToList();

                    foreach (var recs in custlist)
                    {
                        GetRoleMasterDto dto = new GetRoleMasterDto();
                        dto.SysRoleId = recs.CliRoleId;
                        dto.RoleName = recs.RoleName;
                        dto.Description = recs.Description;
                        dto.Comments = recs.Comments;
                        dto.Active = recs.IsActive;
                        returnlist.Add(dto);
                    }
                    svcResponse.Errors = new string[] { };
                    svcResponse.Data = returnlist;
                    connection.Dispose();
                }
            }
            catch (Exception ex)
            {
                svcResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                svcResponse.Errors = new string[] { ex.InnerException.Message };
            }
            return svcResponse;
        }

        public async Task<ServiceResponse<List<GetRoleMasterDto>>> UpdateRole(UpdateRoleMasterDto updatedRole,int Roleid)
        {
            //Log.Information("Entering UpdateUserService", DateTime.UtcNow.ToLongTimeString());
            ServiceResponse<List<GetRoleMasterDto>> svcResponse = new ServiceResponse<List<GetRoleMasterDto>>();

            string connectionString = _context.SysCustomerLinks.Where(c => c.CustomerId == updatedRole.CustomerId).Select(c => c.DbConStr).FirstOrDefault();

            var optionsBuilder = new DbContextOptionsBuilder<GRCDbMasterContext>();
            optionsBuilder.UseNpgsql(connectionString);

            using (var connection = new GRCDbMasterContext(optionsBuilder.Options))
            {
                using (var transaction = connection.Database.BeginTransaction())
                {
                    try
                    {
                        svcResponse.Data = new List<GetRoleMasterDto>();
                        svcResponse.Errors = new String[] { };

                        var tobeupdated = connection.ClientRoleMasters
                            .FirstOrDefault(c => c.CliRoleId == Roleid);

                        if (tobeupdated != null)
                        {
                            // --- Update Address Table ---- 
                            tobeupdated.RoleName = updatedRole.RoleName;
                            tobeupdated.Description = updatedRole.Description;
                            tobeupdated.RoleTypeId = updatedRole.RoleTypeId;
                            tobeupdated.Comments = updatedRole.Comments;
                            tobeupdated.IsActive = updatedRole.Active;

                            connection.ClientRoleMasters.Update(tobeupdated);
                            await connection.SaveChangesAsync();

                            svcResponse.Message = "Role Updated Successfully...!";
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
                        //Log.Error("In Exception: UpdateUserService-", ex.Message);
                    }
                    finally
                    {
                        transaction.Dispose();
                    }
                }
                connection.Dispose();
            }
            //Log.Information("Exiting UpdateUserService", DateTime.UtcNow.ToLongTimeString());
            return svcResponse;
        }

        public async Task<ServiceResponse<List<GetRoleMasterDto>>> DeleteRole(int id , int CustomerId)
        {
            ServiceResponse<List<GetRoleMasterDto>> svcResponse = new ServiceResponse<List<GetRoleMasterDto>>();
            string connectionString = _context.SysCustomerLinks.Where(c => c.CustomerId == CustomerId).Select(c => c.DbConStr).FirstOrDefault();

            var optionsBuilder = new DbContextOptionsBuilder<GRCDbMasterContext>();
            optionsBuilder.UseNpgsql(connectionString);

            using (var connection = new GRCDbMasterContext(optionsBuilder.Options))
            {
                try
                {
                    ConsoleApp1.Models.ClientRoleMaster? tobedeletedcf = connection.ClientRoleMasters
                           .FirstOrDefault(c => c.CliRoleId == id);

                    svcResponse.Errors = Array.Empty<string>();
                    svcResponse.Data = new List<GetRoleMasterDto>();

                    if (tobedeletedcf != null)
                    {
                        //----Deleting in Queue Table--------
                        connection.ClientRoleMasters.Remove(tobedeletedcf);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        svcResponse.StatusCode = (int)HttpStatusCode.NotFound;
                        svcResponse.Message = "Record not found!";
                    }

                    svcResponse.Errors = Array.Empty<string>();
                    svcResponse.Message = "Role Deleted Successfully...!";
                }
                catch (Exception ex)
                {
                    svcResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    svcResponse.Message = ex.Message;
                }
            }
            return svcResponse;
        }
    }
}
