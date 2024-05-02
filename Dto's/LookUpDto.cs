namespace GRCServices.Dto_s
{
    public class StandardLookUpDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }

    }

    public class GovernanceLookUpDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }

    }

    public class RoleTypeLookup
    {
        public int RoleTypeId { get; set; }

        public string? RoleTypeDescription { get; set; }
    }

    public class RoleLookUp
    {
        public int clientRoleId { get; set; }

        public string? RoleName { get; set; }

    }

    public class CustomerLookUp
    {
        public int CusotmerId { get; set; }

        public string? CustomerName { get; set; }

    }

    public class StatusLookUp
    {
        public int Id { get; set; }

        public string? StatusDesc { get; set; }

    }

    public class DoerRoleLookUp
    {
        public int DoerRoleId {  get; set; }

        public string DoerRole { get; set; }
    }

    public class ApproverRoleLookUp
    {
        public int ApproverRoleId { get; set; }

        public string ApproverRole { get; set; }
    }

    public class ApproverLookUp
    {
        public int ApproverId { get; set; }

        public string ApproverName { get; set; }
    }

    public class DoerLookUp
    {
        public int DoerId { get; set; }

        public string DoerName { get; set; }
    }
}