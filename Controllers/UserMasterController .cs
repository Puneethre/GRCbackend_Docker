using GRCServices.Data;
using GRCServices.Dto_s;
using GRCServices.Interfaces;
using GRCServices.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerSetup.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMasterController : ControllerBase
    {
        private readonly IUserMaster _ctrlSvc;
        private readonly GRCDbContext _context;
        public UserMasterController(IUserMaster ctrlSvc, GRCDbContext context)
        {
            _ctrlSvc = ctrlSvc;
            _context = context;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetUserMasterDto>>>> Get(int CustomerId)
        {
            return Ok(await _ctrlSvc.GetAllUsers(CustomerId));
        }


        //[Authorize(Users = "System Admin")]
        //[HttpGet("forsysadmin")]
        //public async Task<ActionResult<ServiceResponse<List<GetUserMasterDto>>>> Getforsys(int userid)
        //{
        //    return Ok(await _ctrlSvc.GetAllCustomersforsysadmin(userid));
        //}

        // POST api/<CustomerController>
        //[Authorize(Users = "System Admin")]
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetUserMasterDto>>>> AddUser(AddUserMasterDto newCustomerDto)
        {
            return Ok(await _ctrlSvc.AddUser(newCustomerDto));
        }

        ////[Authorize(Users = "System Admin,Client Admin,Submitter,Approver,Reviewer")]
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetUserMasterDto>>>> UpdateUser(UpdateUserMasterDto updatedCustomer, int Userid)
        {
            var response = await _ctrlSvc.UpdateUser(updatedCustomer, Userid);

            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")] //Removing delete as this needs to be done through Update.
        public async Task<ActionResult<ServiceResponse<List<GetUserMasterDto>>>> DeleteUser(int id, int CustomerId)
        {
            var response = await _ctrlSvc.DeleteUser(id, CustomerId);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
