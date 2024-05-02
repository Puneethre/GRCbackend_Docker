using AutoMapper;
using ConsoleApp1.Data;
using GRCServices.Data;
using GRCServices.Dto_s;
using GRCServices.Interfaces;
using GRCServices.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GRCServices.Services
{
    public class AssignmentMasterService : IAssignmentMaster
    {
        private readonly IMapper _mapper;
        private readonly GRCDbContext _context;
        //private readonly ILogger<CustomerService> _logger; ILogger<CustomerService> logger
        public AssignmentMasterService(GRCDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetAssignmentMasterDto>>> AddAssignment(AddAssignmentMasterDto newAssignmentMasterdto)
        {
            var svcResponse = new ServiceResponse<List<GetAssignmentMasterDto>>();
            string connectionString = _context.SysCustomerLinks.Where(c => c.CustomerId == newAssignmentMasterdto.CustomerId).Select(c => c.DbConStr).FirstOrDefault();

            var optionsBuilder = new DbContextOptionsBuilder<GRCDbMasterContext>();
            optionsBuilder.UseNpgsql(connectionString);

            using (var connection = new GRCDbMasterContext(optionsBuilder.Options))
            {
                using (var transaction = connection.Database.BeginTransaction())
                {
                    try
                    {
                        //var count = _context.ClientRoleMasters.Where(c => c.RoleName == newRoleMasterdto.RoleName).Count();
                        //if (count != 0)
                        //{
                        //    Exception e = new Exception("Role Name Sholud be Unique");
                        //    throw new Exception("", e);
                        //}

                        //-------Update Customer Main Table-------
                        ConsoleApp1.Models.AssignmentMaster newAssignment = _mapper.Map<ConsoleApp1.Models.AssignmentMaster>(newAssignmentMasterdto);

                        svcResponse.Data = new List<GetAssignmentMasterDto>();
                        svcResponse.Errors = Array.Empty<string>();

                        int nRecs = connection.AssignmentMasters.Count();
                        if (nRecs > 0)
                            newAssignment.Id = connection.AssignmentMasters.Max(c => c.Id) + 1;
                        else
                            newAssignment.Id = 1;   //Start with 1.

                        connection.AssignmentMasters.Add(newAssignment);
                        await connection.SaveChangesAsync();

                        svcResponse.Message = "Assignment Created Successfully...!";
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

        public async Task<ServiceResponse<List<GetAssignmentMasterDto>>> GetAllAssignments(int CustomerId)
        {
            var svcResponse = new ServiceResponse<List<GetAssignmentMasterDto>>();
            string connectionString = _context.SysCustomerLinks.Where(c => c.CustomerId == CustomerId).Select(c => c.DbConStr).FirstOrDefault();

            var optionsBuilder = new DbContextOptionsBuilder<GRCDbMasterContext>();
            optionsBuilder.UseNpgsql(connectionString);

            using (var connection = new GRCDbMasterContext(optionsBuilder.Options))
            {
                try
                {
                    List<GetAssignmentMasterDto> returnlist = new List<GetAssignmentMasterDto>();

                    var custlist = connection.AssignmentMasters
                          //.Where(c => c.Active == 'Y' && c.RoleTypeId != 0)
                          .OrderByDescending(c => c.Id)
                          .ToList();

                    foreach (var recs in custlist)
                    {
                        GetAssignmentMasterDto dto = new GetAssignmentMasterDto();
                        dto.Id = recs.Id;
                       // dto.ActivityName = connection.ActivityMasters.Where(a => a.Id == recs.ActivityMasterId).Select(a => a.ActivityName).FirstOrDefault();
                        dto.User = connection.ClientUserInfos.Where(c => c.CliUserId == recs.DoerCliUserId).Select(c => c.Name).FirstOrDefault();
                        dto.StartDate = recs.StartDate;
                        dto.EndDate = recs.EndDate;
                        dto.Doerstatus = recs.DoerStatus;
                        dto.AuditCheck = recs.AuditCheck;
                        dto.ApprovalStatus = recs.ApprovalStatus;
                        dto.ApprovalDate = recs.ApprovalDate;
                        dto.Approver = connection.ClientUserInfos.Where(c => c.CliUserId == recs.ApproverCliUserId).Select(c => c.Name).FirstOrDefault();
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
            }
            return svcResponse;
        }

        public async Task<ServiceResponse<List<GetAssignmentMasterDto>>> DeleteAssignment(int id,int CustomerId)
        {
            ServiceResponse<List<GetAssignmentMasterDto>> svcResponse = new ServiceResponse<List<GetAssignmentMasterDto>>();
            string connectionString = _context.SysCustomerLinks.Where(c => c.CustomerId == CustomerId).Select(c => c.DbConStr).FirstOrDefault();

            var optionsBuilder = new DbContextOptionsBuilder<GRCDbMasterContext>();
            optionsBuilder.UseNpgsql(connectionString);

            using (var connection = new GRCDbMasterContext(optionsBuilder.Options))
            {
                try
                {
                    ConsoleApp1.Models.AssignmentMaster? tobedeletedcf = connection.AssignmentMasters
                           .FirstOrDefault(c => c.Id == id);

                    svcResponse.Errors = Array.Empty<string>();
                    svcResponse.Data = new List<GetAssignmentMasterDto>();

                    if (tobedeletedcf != null)
                    {
                        //----Deleting in Assignment Table--------
                        connection.AssignmentMasters.Remove(tobedeletedcf);
                        await connection.SaveChangesAsync();
                    }
                    else
                    {
                        svcResponse.StatusCode = (int)HttpStatusCode.NotFound;
                        svcResponse.Message = "Record not found!";
                    }

                    svcResponse.Errors = Array.Empty<string>();
                    svcResponse.Message = "Assignment Deleted Successfully...!";
                }
                catch (Exception ex)
                {
                    svcResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    svcResponse.Message = ex.Message;
                }
            }
            return svcResponse;
        }

        public async Task<ServiceResponse<List<GetAssignmentMasterDto>>> UpdateAssignment(UpdateAssignmentMasterDto updatedAssignment, int AssignmentId)
        {
            //Log.Information("Entering UpdateUserService", DateTime.UtcNow.ToLongTimeString());
            ServiceResponse<List<GetAssignmentMasterDto>> svcResponse = new ServiceResponse<List<GetAssignmentMasterDto>>();
            string connectionString = _context.SysCustomerLinks.Where(c => c.CustomerId == updatedAssignment.CustomerId).Select(c => c.DbConStr).FirstOrDefault();

            var optionsBuilder = new DbContextOptionsBuilder<GRCDbMasterContext>();
            optionsBuilder.UseNpgsql(connectionString);

            using (var connection = new GRCDbMasterContext(optionsBuilder.Options))
            {
                using (var transaction = connection.Database.BeginTransaction())
                {
                    try
                    {
                        svcResponse.Data = new List<GetAssignmentMasterDto>();
                        svcResponse.Errors = new String[] { };

                        var tobeupdated = connection.AssignmentMasters
                            .FirstOrDefault(c => c.Id == AssignmentId);

                        if (tobeupdated != null)
                        {
                            // --- Update Address Table ---- 
                            tobeupdated.ActivityMasterId = updatedAssignment.ActivityId;
                            tobeupdated.DoerCliUserId = updatedAssignment.UserId;
                            tobeupdated.StartDate = updatedAssignment.StartDate;
                            tobeupdated.EndDate = updatedAssignment.EndDate;
                            tobeupdated.DoerStatus = updatedAssignment.Doerstatus;
                            tobeupdated.AuditCheck = updatedAssignment.AuditCheck;
                            tobeupdated.ApprovalStatus = updatedAssignment.ApprovalStatus;
                            tobeupdated.ApprovalDate = updatedAssignment.ApprovalDate;
                            tobeupdated.ApproverCliUserId = updatedAssignment.ApproverId;
                            connection.AssignmentMasters.Update(tobeupdated);
                            await _context.SaveChangesAsync();

                            svcResponse.Message = "Assignment Updated Successfully...!";
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
            }
            //Log.Information("Exiting UpdateUserService", DateTime.UtcNow.ToLongTimeString());
            return svcResponse;
        }

        public async Task<ServiceResponse<List<GetAssignmentMasterForUserDto>>> GetAllAssignmentsForUser(int CustomerId)
        {
            var svcResponse = new ServiceResponse<List<GetAssignmentMasterForUserDto>>();
            string connectionString = _context.SysCustomerLinks.Where(c => c.CustomerId == CustomerId).Select(c => c.DbConStr).FirstOrDefault();

            var optionsBuilder = new DbContextOptionsBuilder<GRCDbMasterContext>();
            optionsBuilder.UseNpgsql(connectionString);

            using (var connection = new GRCDbMasterContext(optionsBuilder.Options))
            {
                try
                {
                    List<GetAssignmentMasterForUserDto> returnlist = new List<GetAssignmentMasterForUserDto>();

                    var custlist = connection.AssignmentMasters
                          //.Where(c => c.Active == 'Y' && c.RoleTypeId != 0)
                          .OrderByDescending(c => c.Id)
                          .ToList();

                    foreach (var recs in custlist)
                    {
                        GetAssignmentMasterForUserDto dto = new GetAssignmentMasterForUserDto();
                        dto.Id = recs.Id;
                       // dto.ActivityName = connection.ActivitiyNameMasters.Where(a => a.Id == recs.).Select(a => a.ActivityName).FirstOrDefault();
                        dto.ActivityDescr = connection.ActivityMasters.Where(a => a.Id == recs.ActivityMasterId).Select(a => a.ActivityDescr).FirstOrDefault();
                        dto.DoerComments = recs.DoerComments;
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
            }
            return svcResponse;
        }

        public async Task<ServiceResponse<List<GetAssignmentMasterForUserDto>>> GetAllAssignmentsForApprover(int CustomerId)
        {
            var svcResponse = new ServiceResponse<List<GetAssignmentMasterForUserDto>>();
            string connectionString = _context.SysCustomerLinks.Where(c => c.CustomerId == CustomerId).Select(c => c.DbConStr).FirstOrDefault();

            var optionsBuilder = new DbContextOptionsBuilder<GRCDbMasterContext>();
            optionsBuilder.UseNpgsql(connectionString);

            using (var connection = new GRCDbMasterContext(optionsBuilder.Options))
            {
                try
                {
                    List<GetAssignmentMasterForUserDto> returnlist = new List<GetAssignmentMasterForUserDto>();

                    var custlist = connection.AssignmentMasters
                          //.Where(c => c.Active == 'Y' && c.RoleTypeId != 0)
                          .OrderByDescending(c => c.Id)
                          .ToList();

                    foreach (var recs in custlist)
                    {
                        GetAssignmentMasterForUserDto dto = new GetAssignmentMasterForUserDto();
                        dto.Id = recs.Id;
                        //dto.ActivityName = connection.ActivityMasters.Where(a => a.Id == recs.ActivityMasterId).Select(a => a.ActivityNameId).FirstOrDefault();
                        dto.ActivityDescr = connection.ActivityMasters.Where(a => a.Id == recs.ActivityMasterId).Select(a => a.ActivityDescr).FirstOrDefault();
                        dto.DoerComments = recs.DoerComments;
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
            }
            return svcResponse;
        }
    }
}
