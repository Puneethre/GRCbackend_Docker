using AutoMapper;
using GRCServices.Data;
using GRCServices.Dto_s;
using GRCServices.Interfaces;
using GRCServices.Models;
using Microsoft.EntityFrameworkCore;
//using Serilog;
using System.Net;

namespace LicenseManagement.Services
{
    public class LicenseManagementService : ILicenseManagement
    {
        private readonly IMapper _mapper;
        private readonly GRCDbContext _context;
        public readonly IConfiguration _configuration;

        public LicenseManagementService(GRCDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<List<GetLicenseDto>>> AddLicense(AddLicenseDto newLicenseDto)
        {
            //Log.Information("Entering AddLicense", DateTime.UtcNow.ToLongTimeString());
            var svcResponse = new ServiceResponse<List<GetLicenseDto>>();

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {               
                    //svcResponse.Errors = new string[] {};
                    SysLicenseMaster newLisence = _mapper.Map<SysLicenseMaster>(newLicenseDto);
                    int nRecs = _context.SysLicenseMasters.Count();
                    if (nRecs > 0)
                        newLisence.LicnId = _context.SysLicenseMasters.Max(c => c.LicnId) + 1;
                    else
                        newLisence.LicnId = 1;   //Start with 1.
     
                    _context.SysLicenseMasters.Add(newLisence);
                    await _context.SaveChangesAsync();

                    svcResponse.Message =  "Lisence Created Successfully...!" ;

                    svcResponse.Data = new List<GetLicenseDto>();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    svcResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    svcResponse.Message =  ex.Message;
                    svcResponse.Errors = new string[] { ex.InnerException.Message == null ?  null : ex.InnerException.Message };
                    //svcResponse.Errors = validationreports.Select(arr => arr.ToString()).ToArray();
                   // Log.Error("In Exception: AddLicense-", ex.Message);
                }
                finally
                {
                    transaction.Dispose();
                }
            }
            //Log.Information("Exiting AddLicense", DateTime.UtcNow.ToLongTimeString());
            return svcResponse;
        }

        //********************************************************  C#  Code  ********************************************************************

        public async Task<ServiceResponse<List<GetLicenseDto>>> GetAllLicense(int? CustomerId)
        {
            //Log.Information("Entering GetAllLicense", DateTime.UtcNow.ToLongTimeString());
            var svcResponse = new ServiceResponse<List<GetLicenseDto>>();
            try
            {
                List<GetLicenseDto> dto = new List<GetLicenseDto>();

                var query = _context.SysLicenseMasters
                    .OrderByDescending(l => l.LicnId)
                    .AsQueryable();

                if (CustomerId.HasValue)
                {
                    query = query.Where(l => l.CustomerId == CustomerId);
                }

                var dbRecs = await query
                    .Select(c => new
                    {
                        c.LicnId,
                        c.ContractPeriodInMonths,
                        c.StartOrRenewalDate,
                        c.EndDate,
                        c.CustomerId,
                        c.ContractDocuments,
                        c.StandardId,
                        c.Approved,
                        c.Remarks,
                        c.IsActive,
                        c.CreatedBy,
                        c.CreatedDate,
                        c.EditedBy,
                        c.EditedDate,
                        c.ApprovedBy, 
                        c.ApprovedDate
                    })
                    .ToListAsync();

                foreach (var rec in dbRecs)
                {
                    var CustNames = _context.SysCustomerInfos.FirstOrDefault(c => c.CustomerId == rec.CustomerId)?.CustomerName;
                    //var GovNames = _context.GovernanceMasters.FirstOrDefault(f => f.GovId == rec.LisenceGovernanceId)?.GovName;
                    var StdNames = _context.StandardMasters.FirstOrDefault(s => s.Id == rec.StandardId)?.Name;

                    GetLicenseDto gl = new GetLicenseDto
                    {
                        LicenseId = rec.LicnId,
                        ContractPeriodInMonths = rec.ContractPeriodInMonths,
                        StartOrRenewalDate = rec.StartOrRenewalDate,
                        EndDate = rec.EndDate,
                        CustomerId = rec.CustomerId,
                        CustomerName = CustNames,
                        StandardId = rec.StandardId,
                        StandardName = StdNames,
                        Approved = rec.Approved,
                        Remarks = rec.Remarks,
                        ContractDocuments = rec.ContractDocuments,
                        Active = rec.IsActive,
                        CreatedBy = rec.CreatedBy, 
                        CreatedDate = rec.CreatedDate,
                        EditedBy = rec.EditedBy,
                        EditedDate = rec.EditedDate,
                        ApprovedBy = rec.ApprovedBy,
                        ApprovedDate = rec.ApprovedDate
                    };

                    dto.Add(gl);
                }

                svcResponse.Data = dto;
            }
            catch (Exception ex)
            {
                svcResponse.Message = ex.InnerException?.Message ?? ex.Message;
                svcResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                //Log.Error("In Exception: GetAllLicense-", ex.Message);
                svcResponse.Errors = new string[] { ex.InnerException?.Message ?? ex.Message };
            }
            //Log.Information("Exiting GetAllLicense", DateTime.UtcNow.ToLongTimeString());
            return svcResponse;
        }

        public async Task<ServiceResponse<List<GetLicenseDto>>> UpdateLicense(UpdateLicenseDto updateLicense)
        {
           // Log.Information("Entering UpdateFormService", DateTime.UtcNow.ToLongTimeString());
            var svcResponse = new ServiceResponse<List<GetLicenseDto>>();
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var tobeupdated = await _context.SysLicenseMasters
                                                .FirstOrDefaultAsync(c => c.LicnId == updateLicense.LicenseId);

                    if (tobeupdated != null /*&& updatedForm.IsDeleted == false*/)
                    {
                        //--- Update Form Table ---- 
                        tobeupdated.StartOrRenewalDate = updateLicense.StartOrRenewalDate;
                        tobeupdated.ContractPeriodInMonths = updateLicense.ContractPeriodInMonths;
                        tobeupdated.EndDate = updateLicense.EndDate;
                        tobeupdated.EditedDate = DateTime.UtcNow;
                        tobeupdated.ContractDocuments = updateLicense.ContractDocuments;
                        tobeupdated.CustomerId = updateLicense.CustomerId;
                        tobeupdated.StandardId = updateLicense.StandardId;
                        tobeupdated.EditedBy = updateLicense.EditedBy;
                        tobeupdated.Remarks = updateLicense.Remarks;
                        await _context.SaveChangesAsync();

                        svcResponse.Message = "License Updated Successfully...!";
                    }
                    else if (tobeupdated == null)
                    {
                        svcResponse.StatusCode = (int)HttpStatusCode.NotFound;
                        svcResponse.Message = $"License with ID {updateLicense.LicenseId} does not exist.";
                    }
                    svcResponse.Data = new List<GetLicenseDto>();
                    svcResponse.Errors = Array.Empty<string>();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    svcResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    svcResponse.Message = "Error Occurred While Updating License";
                    svcResponse.Errors = new string[] { ex.InnerException.Message };
                   // Log.Error("In Exception: UpdateLicenseService-", ex.Message);
                }
                finally
                {
                    transaction.Dispose();
                }

                //Log.Information("Exiting UpdateLicenseService", DateTime.UtcNow.ToLongTimeString());
                return svcResponse;
            }
        }
    }
}
