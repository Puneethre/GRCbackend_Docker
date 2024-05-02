using System.ComponentModel;

namespace GRCServices.Dto_s
{
    public class response
    {
        public int? userid { get; set; }
        public string Message { get; set; }
        public bool? ChangePasswordFlag { get; set; }
    }

    public class validationresponse
    {
        public string username { get; set; }
        public int? sysRoleId { get; set; }
        public int? UserId { get; set; }
        //public bool ismultiplecustomers { get; set; }
        //public List<Customer> Customers { get; set; }
        public string Message { get; set; }
        public string AuthenticationToken { get; set; }

        public bool isClientAdmin { get; set; }

        public bool isSystemAdmin { get; set; }

        public int? CustomerId {  get; set; }

    }

    public class Customer
    {
        public int customerId { get; set; }
        public string CustomerName { get; set; }
        public string? FontColor { get; set; }
        public string? SecondaryColor { get; set; }
        public string? PrimaryColor { get; set; }
    }

    public class creditials
    {
        public string email { get; set; }

        public string password { get; set; }
    }

    public class resetpayload
    {
        public string email { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }

    public class uidresponse
    {
        public bool isUiDExist { get; set; }
    }
    public class validatecreditials
    {
        public int userid { get; set; }

        public string code { get; set; }
    }
}
