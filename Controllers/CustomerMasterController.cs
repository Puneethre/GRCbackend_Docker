using GRCServices.Data;
using GRCServices.Dto_s;
using GRCServices.Interfaces;
using GRCServices.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GRCServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerMasterController : ControllerBase
    {
        private readonly ICustomerMaster _ctrlSvc;
        private readonly GRCDbContext _context;
        public CustomerMasterController(ICustomerMaster ctrlSvc, GRCDbContext context)
        {
            _ctrlSvc = ctrlSvc;
            _context = context;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetCustomerMasterDto>>>> Get()
        {
            return Ok(await _ctrlSvc.GetAllCustomers());
        }

        //[Authorize(Customers = "System Admin")]
        //[HttpGet("forsysadmin")]
        //public async Task<ActionResult<ServiceResponse<List<GetCustomerMasterDto>>>> Getforsys(int userid)
        //{
        //    return Ok(await _ctrlSvc.GetAllCustomersforsysadmin(userid));
        //}

        // POST api/<CustomerController>
        //[Authorize(Customers = "System Admin")]
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCustomerMasterDto>>>> AddCustomer(AddCustomerMasterDto newCustomerDto)
        {
            return Ok(await _ctrlSvc.AddCustomer(newCustomerDto));
        }

        //[Authorize(Customers = "System Admin,Client Admin,Submitter,Approver,Reviewer")]
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetCustomerMasterDto>>>> UpdateCustomer(UpdateCustomerMasterDto updatedCustomer, int Customerid)
        {
            var response = await _ctrlSvc.UpdateCustomer(updatedCustomer, Customerid);

            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")] //Removing delete as this needs to be done through Update.
        public async Task<ActionResult<ServiceResponse<List<GetCustomerMasterDto>>>> DeleteCustomer(int id)
        {
            var response = await _ctrlSvc.DeleteCustomer(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
