using ConsoleApp1.Data;
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
    public class RoleMasterController : ControllerBase
    {
        private readonly IRoleMaster _ctrlSvc;
        private readonly GRCDbContext _context;
        public RoleMasterController(IRoleMaster ctrlSvc, GRCDbContext context)
        {
            _ctrlSvc = ctrlSvc;
            _context = context;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetRoleMasterDto>>>> Get(int CustomerId)
        {
            return Ok(await _ctrlSvc.GetAllRoles(CustomerId));
        }

        [HttpGet("roletypelookup")]
        public async Task<ActionResult<List<RoleTypeLookup>>> Getroletypess(int CustomerId)
        {
            string connectionString = _context.SysCustomerLinks.Where(c => c.CustomerId == CustomerId).Select(c => c.DbConStr).FirstOrDefault();

            var optionsBuilder = new DbContextOptionsBuilder<GRCDbMasterContext>();
            optionsBuilder.UseNpgsql(connectionString);

            using (var connection = new GRCDbMasterContext(optionsBuilder.Options))
            {
                try
                {
                    var cutomeslist = connection.RoleTypes.ToList();

                    List<RoleTypeLookup> roletypes = new List<RoleTypeLookup>();
                    foreach (var rec in cutomeslist)
                    {
                        RoleTypeLookup rtl = new RoleTypeLookup();
                        rtl.RoleTypeId = rec.Id;
                        rtl.RoleTypeDescription = rec.RoleTypeDesc;
                        roletypes.Add(rtl);
                    }

                    return Ok(roletypes);
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }
            }
        }

        [HttpGet("roleslookup")]
        public async Task<ActionResult<List<RoleLookUp>>> Getrolelookup(int CustomerId)
        {
            string connectionString = _context.SysCustomerLinks.Where(c => c.CustomerId == CustomerId).Select(c => c.DbConStr).FirstOrDefault();

            var optionsBuilder = new DbContextOptionsBuilder<GRCDbMasterContext>();
            optionsBuilder.UseNpgsql(connectionString);

            using (var connection = new GRCDbMasterContext(optionsBuilder.Options))
            {
                try
                {
                    var cutomeslist = connection.ClientRoleMasters.Where(r => r.RoleTypeId != 0).ToList();

                    List<RoleLookUp> roles = new List<RoleLookUp>();
                    foreach (var rec in cutomeslist)
                    {
                        RoleLookUp rtl = new RoleLookUp();
                        rtl.clientRoleId = rec.CliRoleId;
                        rtl.RoleName = rec.RoleName;
                        roles.Add(rtl);
                    }

                    return Ok(roles);
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }
            }
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
        public async Task<ActionResult<ServiceResponse<List<GetRoleMasterDto>>>> AddRole(AddRoleMasterDto newCustomerDto)
        {
            return Ok(await _ctrlSvc.AddRole(newCustomerDto));
        }

        //[Authorize(Roles = "System Admin,Client Admin,Submitter,Approver,Reviewer")]
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetRoleMasterDto>>>> UpdateRole(UpdateRoleMasterDto updatedCustomer, int Roleid)
        {
            var response = await _ctrlSvc.UpdateRole(updatedCustomer, Roleid);

            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")] //Removing delete as this needs to be done through Update.
        public async Task<ActionResult<ServiceResponse<List<GetRoleMasterDto>>>> DeleteRole(int id, int CustomerId)
        {
            var response = await _ctrlSvc.DeleteRole(id,CustomerId);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
