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
    public class AssignmentMasterController : ControllerBase
    {
        private readonly IAssignmentMaster _ctrlSvc;
        private readonly GRCDbContext _context;
        public AssignmentMasterController(IAssignmentMaster ctrlSvc, GRCDbContext context)
        {
            _ctrlSvc = ctrlSvc;
            _context = context;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetAssignmentMasterDto>>>> Get(int CustomerId)
        {
            return Ok(await _ctrlSvc.GetAllAssignments(CustomerId));
        }


        //[Authorize(Assignments = "System Admin")]
        //[HttpGet("forsysadmin")]
        //public async Task<ActionResult<ServiceResponse<List<GetAssignmentMasterDto>>>> Getforsys(int Assignmentid)
        //{
        //    return Ok(await _ctrlSvc.GetAllCustomersforsysadmin(Assignmentid));
        //}

        // POST api/<CustomerController>
        //[Authorize(Assignments = "System Admin")]
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetAssignmentMasterDto>>>> AddAssignment(AddAssignmentMasterDto newCustomerDto)
        {
            return Ok(await _ctrlSvc.AddAssignment(newCustomerDto));
        }

        ////[Authorize(Assignments = "System Admin,Client Admin,Submitter,Approver,Reviewer")]
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetAssignmentMasterDto>>>> UpdateAssignment(UpdateAssignmentMasterDto updatedCustomer, int Assignmentid)
        {
            var response = await _ctrlSvc.UpdateAssignment(updatedCustomer, Assignmentid);

            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")] //Removing delete as this needs to be done through Update.
        public async Task<ActionResult<ServiceResponse<List<GetAssignmentMasterDto>>>> DeleteAssignment(int id, int CustomerId)
        {
            var response = await _ctrlSvc.DeleteAssignment(id, CustomerId);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        } 
    }
}
