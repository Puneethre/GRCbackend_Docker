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
    public class LookUpController : ControllerBase
    {
        private readonly GRCDbContext _context;
        public LookUpController(GRCDbContext context)
        {
            _context = context;
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

        [HttpGet("StandardMasterlookup")]
        public async Task<ActionResult<List<StandardLookUpDto>>> GetStandard(int governaceId)
        {
            try
            {
                var standardlist = await _context.StandardMasters.Where(s => s.GovrId == governaceId).ToListAsync();

                List<StandardLookUpDto> standard = new List<StandardLookUpDto>();
                foreach (var rec in standardlist)
                {
                    StandardLookUpDto sl = new StandardLookUpDto();
                    sl.Id = rec.Id;
                    sl.Name = rec.Name;
                    standard.Add(sl);
                }

                return Ok(standard);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpGet("GovernanceMasterlookup")]
        public async Task<ActionResult<List<GovernanceLookUpDto>>> GetGovernance()
        {
            try
            {
                var govlist = await _context.GovernanceMasters.ToListAsync();

                List<GovernanceLookUpDto> Govs = new List<GovernanceLookUpDto>();
                foreach (var rec in govlist)
                {
                    GovernanceLookUpDto gl = new GovernanceLookUpDto();
                    gl.Id = rec.Id;
                    gl.Name = rec.Name;
                    Govs.Add(gl);
                }

                return Ok(Govs);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("Customerlookup")]
        public async Task<ActionResult<List<CustomerLookUp>>> GetCustomers()
        {
            try
            {
                var govlist = await _context.SysCustomerInfos.ToListAsync();

                List<CustomerLookUp> Customers = new List<CustomerLookUp>();
                foreach (var rec in govlist)
                {
                    CustomerLookUp cl = new CustomerLookUp();
                    cl.CusotmerId = rec.CustomerId;
                    cl.CustomerName = rec.CustomerName;
                    Customers.Add(cl);
                }

                return Ok(Customers);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("Statuslookup")]
        public async Task<ActionResult<List<StatusLookUp>>> GetStatus()
        {
            try
            {
                var govlist = await _context.StatusMasters.ToListAsync();

                List<StatusLookUp> stslist = new List<StatusLookUp>();
                foreach (var rec in govlist)
                {
                    StatusLookUp cl = new StatusLookUp();
                    cl.Id = rec.Id;
                    cl.StatusDesc = rec.Status;
                    stslist.Add(cl);
                }

                return Ok(stslist);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("Doerslookup")]
        public async Task<ActionResult<List<DoerLookUp>>> GetDoers(int CustomerId, int Roleid)
        {
            string connectionString = _context.SysCustomerLinks.Where(c => c.CustomerId == CustomerId).Select(c => c.DbConStr).FirstOrDefault();

            var optionsBuilder = new DbContextOptionsBuilder<GRCDbMasterContext>();
            optionsBuilder.UseNpgsql(connectionString);

            using (var connection = new GRCDbMasterContext(optionsBuilder.Options))
            {
                try
                {
                    var cutomeslist = connection.ClientUserInfos.Where(r => r.CliRoleId == Roleid).ToList();

                    List<DoerLookUp> approvers = new List<DoerLookUp>();
                    foreach (var rec in cutomeslist)
                    {
                        DoerLookUp atl = new DoerLookUp();
                        atl.DoerId = rec.CliUserId;
                        atl.DoerName = rec.Name;
                        approvers.Add(atl);
                    }

                    return Ok(approvers);
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }
            }
        }

        [HttpGet("DoerRolelookup")]
        public async Task<ActionResult<List<DoerRoleLookUp>>> GetDoerRoles(int CustomerId)
        {
            string connectionString = _context.SysCustomerLinks.Where(c => c.CustomerId == CustomerId).Select(c => c.DbConStr).FirstOrDefault();

            var optionsBuilder = new DbContextOptionsBuilder<GRCDbMasterContext>();
            optionsBuilder.UseNpgsql(connectionString);

            using (var connection = new GRCDbMasterContext(optionsBuilder.Options))
            {
                try
                {
                    var cutomeslist = connection.ClientRoleMasters.Where(r => r.RoleTypeId != 0).ToList();

                    List<DoerRoleLookUp> doers = new List<DoerRoleLookUp>();
                    foreach (var rec in cutomeslist)
                    {
                        DoerRoleLookUp rtl = new DoerRoleLookUp();
                        rtl.DoerRoleId = rec.CliRoleId;
                        rtl.DoerRole = rec.RoleName;
                        doers.Add(rtl);
                    }

                    return Ok(doers);
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }
            }
        }

        [HttpGet("Approverslookup")]
        public async Task<ActionResult<List<ApproverLookUp>>> GetApprovers(int CustomerId , int Roleid)
        {
            string connectionString = _context.SysCustomerLinks.Where(c => c.CustomerId == CustomerId).Select(c => c.DbConStr).FirstOrDefault();

            var optionsBuilder = new DbContextOptionsBuilder<GRCDbMasterContext>();
            optionsBuilder.UseNpgsql(connectionString);

            using (var connection = new GRCDbMasterContext(optionsBuilder.Options))
            {
                try
                {
                    var cutomeslist = connection.ClientUserInfos.Where(r => r.CliRoleId == Roleid).ToList();

                    List<ApproverLookUp> approvers = new List<ApproverLookUp>();
                    foreach (var rec in cutomeslist)
                    {
                        ApproverLookUp atl = new ApproverLookUp();
                        atl.ApproverId = rec.CliUserId;
                        atl.ApproverName = rec.Name;
                        approvers.Add(atl);
                    }

                    return Ok(approvers);
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }
            }
        }

        [HttpGet("ApproverRolelookup")]
        public async Task<ActionResult<List<ApproverRoleLookUp>>> GetApproverRoles(int CustomerId)
        {
            string connectionString = _context.SysCustomerLinks.Where(c => c.CustomerId == CustomerId).Select(c => c.DbConStr).FirstOrDefault();

            var optionsBuilder = new DbContextOptionsBuilder<GRCDbMasterContext>();
            optionsBuilder.UseNpgsql(connectionString);

            using (var connection = new GRCDbMasterContext(optionsBuilder.Options))
            {
                try
                {
                    var cutomeslist = connection.ClientRoleMasters.Where(r => r.RoleTypeId != 0).ToList();

                    List<ApproverRoleLookUp> approvers = new List<ApproverRoleLookUp>();
                    foreach (var rec in cutomeslist)
                    {
                        ApproverRoleLookUp atl = new ApproverRoleLookUp();
                        atl.ApproverRoleId = rec.CliRoleId;
                        atl.ApproverRole = rec.RoleName;
                        approvers.Add(atl);
                    }

                    return Ok(approvers);
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }
            }
        }

        [HttpGet("ActivityNameslookup")]
        public async Task<ActionResult<List<ApproverRoleLookUp>>> GetActivities(int CustomerId)
        {
            string connectionString = _context.SysCustomerLinks.Where(c => c.CustomerId == CustomerId).Select(c => c.DbConStr).FirstOrDefault();

            var optionsBuilder = new DbContextOptionsBuilder<GRCDbMasterContext>();
            optionsBuilder.UseNpgsql(connectionString);

            using (var connection = new GRCDbMasterContext(optionsBuilder.Options))
            {
                try
                {
                    var cutomeslist = connection.ClientRoleMasters.Where(r => r.RoleTypeId != 0).ToList();

                    List<ApproverRoleLookUp> approvers = new List<ApproverRoleLookUp>();
                    foreach (var rec in cutomeslist)
                    {
                        ApproverRoleLookUp atl = new ApproverRoleLookUp();
                        atl.ApproverRoleId = rec.CliRoleId;
                        atl.ApproverRole = rec.RoleName;
                        approvers.Add(atl);
                    }

                    return Ok(approvers);
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }
            }
        }
    }
}
