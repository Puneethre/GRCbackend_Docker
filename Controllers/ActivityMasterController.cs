using GRCServices.Dto_s;
using GRCServices.Interfaces;
using GRCServices.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GRCServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityMasterController : ControllerBase
    {
        private readonly IActivityMaster _ctrlSvc;
        public ActivityMasterController(IActivityMaster ctrlSvc)
        {
            _ctrlSvc = ctrlSvc;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetActivityMasterDto>>>> Get(int CustomerId)
        {
            return Ok(await _ctrlSvc.GetAllActivites(CustomerId));
        }

        //[Authorize(Roles = "System Admin")]
        //[HttpGet("forsysadmin")]
        //public async Task<ActionResult<ServiceResponse<List<GetRoleMasterDto>>>> Getforsys(int userid)
        //{
        //    return Ok(await _ctrlSvc.GetAllCustomersforsysadmin(userid));
        //}

        // POST api/<CustomerController>
        //[Authorize(Roles = "System Admin")]
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetActivityMasterDto>>>> AddRole(AddActivityMasterDto newActivityDto)
        {
            return Ok(await _ctrlSvc.AddActivity(newActivityDto));
        }

        //[Authorize(Roles = "System Admin,Client Admin,Submitter,Approver,Reviewer")]
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetActivityMasterDto>>>> UpdateRole(UpdateActivityMasterDto updatedActivity, int Roleid)
        {
            var response = await _ctrlSvc.UpdateActivity(updatedActivity, Roleid);

            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")] //Removing delete as this needs to be done through Update.
        public async Task<ActionResult<ServiceResponse<List<GetActivityMasterDto>>>> DeleteRole(int id, int CustomerId)
        {
            var response = await _ctrlSvc.DeleteActivity(id, CustomerId);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
