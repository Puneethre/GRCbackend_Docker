namespace GRCServices.Dto_s
{
    public class GetUserMasterDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? CustomerName { get; set; }

        public string PhoneNo { get; set; }

        public string? Role { get; set; }

        public bool Status { get; set; }
    }

    public class AddUserMasterDto
    {
        //public int Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public int CustomerId { get; set; }

        public string PhoneNo { get; set; }

        public int CliRoleId { get; set; }

        public bool Status { get; set; }

        public int CreatedBy { get; set; }
    }

    public class UpdateUserMasterDto
    {
        //public int Id { get; set; }

        public string? Name { get; set; }

        //public string? Email { get; set; }

        public int? CustomerId { get; set; }

       // public int? SysUserId { get; set; }

        public int CliRoleId { get; set; }

        public bool Status { get; set; }
    }
}
