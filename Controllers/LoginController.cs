using GRCServices.Interfaces;
using GRCServices.Dto_s;
using GRCServices.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GRCServices.Controllers
{
    [Route("api")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginInterface _service;

        public LoginController(LoginInterface service)
        {
            _service = service;
        }

        [HttpPost("userLogin")]
        public async Task<ActionResult<response>> Login(creditials details)
        {
            return Ok(await _service.Authenticate(details.email, details.password));
        }


        [HttpPost("userResetPassword")]
        public async Task<ActionResult<response>> Resetpassword(resetpayload resetpayload)
        {
            return Ok(await _service.ResetPassword(resetpayload.email, resetpayload.OldPassword, resetpayload.NewPassword));
        }

        [HttpPost("userAuthentication")]
        public async Task<ActionResult<validationresponse>> validateMFA(validatecreditials details)
        {
            return Ok(_service.ValidateMFA(details.userid, details.code));
        }

        [HttpPost("SendMFAForForgotPWD")]
        public async Task<ActionResult<response>> SendMFAForForgotPassword(string email)
        {
            return Ok(await _service.SendMFAForForgotPWD(email));
        }

        [HttpPost("VerifyOtp")]
        public async Task<ActionResult<response>> ValidateMFAForForgotPassword(validatecreditials details)
        {
            return Ok(await _service.VerifyOtp(details.userid, details.code));
        }

        [HttpPost("ForgotPassword")]
        public async Task<ActionResult<response>> ForgotPassword(creditials creditials)
        {
            return Ok(await _service.ForgotPassword(creditials.email, creditials.password));
        }

        [HttpGet("isExistUID")]
        public async Task<ActionResult<uidresponse>> isExist(Guid uid)
        {
            return Ok(_service.isUIDExist(uid));
        }
    }
}
