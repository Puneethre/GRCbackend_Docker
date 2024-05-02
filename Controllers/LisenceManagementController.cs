using GRCServices.Dto_s;
using GRCServices.Interfaces;
using GRCServices.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GRCServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LicenseManagementController : ControllerBase
    {
        private readonly ILicenseManagement _ctrlSvc;
        public LicenseManagementController(ILicenseManagement ctrlSvc)
        {
            _ctrlSvc = ctrlSvc;
        }

        // GET: api/<LicenseManagementController>
        [HttpGet("GetAllLicense")]
        public async Task<ActionResult<ServiceResponse<List<GetLicenseDto>>>> GetAllLicense(int? CustomerId)
        {
            return Ok(await _ctrlSvc.GetAllLicense(CustomerId));
        }



        // POST api/<LicenseManagementController>
        [HttpPost("AddLicense")]
        public async Task<ActionResult<ServiceResponse<List<GetLicenseDto>>>> AddLicense(AddLicenseDto newForm)
        {
            return Ok(await _ctrlSvc.AddLicense(newForm));
        }


        // POST api/<LicenseManagementController>
        [HttpPut("UpdateLicense")]
        public async Task<ActionResult<ServiceResponse<List<GetLicenseDto>>>> UpdateLicense(UpdateLicenseDto updateLicense)
        {
            return Ok(await _ctrlSvc.UpdateLicense(updateLicense));
        }


    }
}
