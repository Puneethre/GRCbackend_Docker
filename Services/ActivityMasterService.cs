using AutoMapper;
using ConsoleApp1.Data;
using GRCServices.Data;
using GRCServices.Dto_s;
using GRCServices.Interfaces;
using GRCServices.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.AccessControl;

namespace GRCServices.Services
{
    public class ActivityMasterService : IActivityMaster
    {
        private readonly IMapper _mapper;
        private readonly GRCDbContext _context;
        //private readonly ILogger<ActivityMasterService> _logger;, ILogger<ActivityMasterService> logger
        public ActivityMasterService(GRCDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetActivityMasterDto>>> AddActivity(AddActivityMasterDto newActivityMasterdto)
        {
            var svcResponse = new ServiceResponse<List<GetActivityMasterDto>>();
            string connectionString = _context.SysCustomerLinks.Where(c => c.CustomerId == newActivityMasterdto.CustomerId).Select(c => c.DbConStr).FirstOrDefault();

            var optionsBuilder = new DbContextOptionsBuilder<GRCDbMasterContext>();
            optionsBuilder.UseNpgsql(connectionString);

            using (var connection = new GRCDbMasterContext(optionsBuilder.Options))
            {
                using (var transaction = connection.Database.BeginTransaction())
                {
                    try
                    {
                        //var count = _context.ActivityMasters.Where(c => c.RoleName == newActivityMasterdto.RoleName).Count();
                        //if (count != 0)
                        //{
                        //    Exception e = new Exception("Role Name Sholud be Unique");
                        //    throw new Exception("", e);
                        //}

                        //-------Update Customer Main Table-------
                        ConsoleApp1.Models.ActivityMaster newActivity = _mapper.Map<ConsoleApp1.Models.ActivityMaster>(newActivityMasterdto);

                        svcResponse.Data = new List<GetActivityMasterDto>();
                        svcResponse.Errors = Array.Empty<string>();

                        int nRecs = connection.ActivityMasters.Count();
                        if (nRecs > 0)
                            newActivity.Id = connection.ActivityMasters.Max(c => c.Id) + 1;
                        else
                            newActivity.Id = 1;   //Start with 1.

                        connection.ActivityMasters.Add(newActivity);
                        await _context.SaveChangesAsync();

                        svcResponse.Message = "Activity Created Successfully...!";
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        svcResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                        svcResponse.Message = "Error Occured While Adding Activity..";
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

        public async Task<ServiceResponse<List<GetActivityMasterDto>>> GetAllActivites(int CustomerId)
        {
            var svcResponse = new ServiceResponse<List<GetActivityMasterDto>>();
            string connectionString = _context.SysCustomerLinks.Where(c => c.CustomerId == CustomerId).Select(c => c.DbConStr).FirstOrDefault();

            var optionsBuilder = new DbContextOptionsBuilder<GRCDbMasterContext>();
            optionsBuilder.UseNpgsql(connectionString);

            using (var connection = new GRCDbMasterContext(optionsBuilder.Options))
            {
                try
                {
                    List<GetActivityMasterDto> returnlist = new List<GetActivityMasterDto>();

                    var Activtieslist = connection.ActivityMasters
                          //.Where(c => c.Active == 'Y')
                          .ToList();

                    foreach (var recs in Activtieslist)
                    {
                        GetActivityMasterDto dto = new GetActivityMasterDto();
                        dto.Id = recs.Id;
                        dto.ActivityName = connection.ActivitiyNameMasters.Where(a => a.Id == recs.ActivityNameId).Select(a => a.ActivityName).FirstOrDefault();
                        dto.ActivityDescr = recs.ActivityDescr;
                        dto.DoerRole = recs.DoerRole;
                        dto.Frequency = recs.FrequencyId;
                        dto.Duration = recs.Duration;
                        dto.RefDocument = recs.RefDocumentId;
                        dto.OutputDocument = recs.OutputDocumentPath;
                        dto.TriggeringActivity = recs.TriggeringActivityNameId;
                        dto.ApproverRole = recs.ApproverRole;
                        dto.Auditable = recs.Auditable;
                        dto.HelpRef = recs.HelpRef;
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

        public async Task<ServiceResponse<List<GetActivityMasterDto>>> UpdateActivity(UpdateActivityMasterDto updatedActivity, int ActivityId)
        {
            ServiceResponse<List<GetActivityMasterDto>> svcResponse = new ServiceResponse<List<GetActivityMasterDto>>();
            string connectionString = _context.SysCustomerLinks.Where(c => c.CustomerId == updatedActivity.CustomerId).Select(c => c.DbConStr).FirstOrDefault();

            var optionsBuilder = new DbContextOptionsBuilder<GRCDbMasterContext>();
            optionsBuilder.UseNpgsql(connectionString);

            using (var connection = new GRCDbMasterContext(optionsBuilder.Options))
            {
                using (var transaction = connection.Database.BeginTransaction())
                {
                    try
                    {
                        svcResponse.Data = new List<GetActivityMasterDto>();
                        svcResponse.Errors = new String[] { };

                        var tobeupdated = connection.ActivityMasters
                            .FirstOrDefault(c => c.Id == ActivityId);

                        if (tobeupdated != null)
                        {
                            // --- Update Address Table ---- 
                           // tobeupdated.ActivityNameId = updatedActivity.ActivityName;
                            tobeupdated.ActivityDescr = updatedActivity.ActivityDescr;
                            tobeupdated.FrequencyId = updatedActivity.Frequency;
                            tobeupdated.Duration = updatedActivity.Duration;
                            tobeupdated.DoerRole = updatedActivity.DoerRole;
                            tobeupdated.RefDocumentId = updatedActivity.RefDocument;
                            tobeupdated.OutputDocumentPath = updatedActivity.OutputDocument;
                            tobeupdated.TriggeringActivityNameId = updatedActivity.TriggeringActivity;
                            tobeupdated.ApproverRole = updatedActivity.ApproverRole;
                            tobeupdated.Auditable = updatedActivity.Auditable;
                            tobeupdated.HelpRef = updatedActivity.HelpRef;

                            connection.ActivityMasters.Update(tobeupdated);
                            await _context.SaveChangesAsync();

                            svcResponse.Message = "Activity Updated Successfully...!";
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
                        svcResponse.Message = "Error Occured While Updating Activity...";
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

        public async Task<ServiceResponse<List<GetActivityMasterDto>>> DeleteActivity(int id,int CustomerId)
        {
            ServiceResponse<List<GetActivityMasterDto>> svcResponse = new ServiceResponse<List<GetActivityMasterDto>>();
            string connectionString = _context.SysCustomerLinks.Where(c => c.CustomerId == CustomerId).Select(c => c.DbConStr).FirstOrDefault();

            var optionsBuilder = new DbContextOptionsBuilder<GRCDbMasterContext>();
            optionsBuilder.UseNpgsql(connectionString);

            using (var connection = new GRCDbMasterContext(optionsBuilder.Options))
            {
                try
                {
                    ConsoleApp1.Models.ActivityMaster? tobedeletedcf = connection.ActivityMasters
                           .FirstOrDefault(c => c.Id == id);

                    svcResponse.Errors = Array.Empty<string>();
                    svcResponse.Data = new List<GetActivityMasterDto>();

                    if (tobedeletedcf != null)
                    {
                        //----Deleting in Queue Table--------
                        connection.ActivityMasters.Remove(tobedeletedcf);
                        await connection.SaveChangesAsync();
                    }
                    else
                    {
                        svcResponse.StatusCode = (int)HttpStatusCode.NotFound;
                        svcResponse.Message = "Record not found!";
                    }

                    svcResponse.Errors = Array.Empty<string>();
                    svcResponse.Message = "Activity Deleted Successfully...!";
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
