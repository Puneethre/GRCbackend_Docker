using GRCServices.Dto_s;

namespace GRCServices.Interfaces
{
    public interface LoginInterface
    {
        public Task<response> Authenticate(string email, string password);

        //public bool IsValidUser(string username, string password);

        public validationresponse ValidateMFA(int userid, string code);

        public Task<response> ResetPassword(string email, string OldPassword ,string NewPassword);

        public Task<response> ForgotPassword(string email, string newPassword);

        public Task<response> SendMFAForForgotPWD(string email);

        public Task<response> VerifyOtp(int userid, string code);

        public uidresponse isUIDExist(Guid uid);
    }
}
