namespace GRCServices.Dto_s
{
    public class GetLicenseInputDto
    {
        public int? CustomerId { get; set; }
    }
    public class GetLicenseDto
    {
        public int LicenseId { get; set; }
        public DateOnly? StartOrRenewalDate { get; set; }
        public int? ContractPeriodInMonths { get; set; }
        public DateOnly? EndDate { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public int? StandardId { get; set; }
        public string? StandardName { get; set; }
        public bool? Approved { get; set; }
        public string? ContractDocuments { get; set; }
        public string? Remarks { get; set; }
        public bool? Active { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }

    }

    public class AddLicenseDto
    {
        //public int LisenceId { get; set; }
        public DateOnly? StartOrRenewalDate { get; set; }
        public int? ContractPeriodInMonths { get; set; }
        public DateOnly? EndDate { get; set; }
        public int? CustomerId { get; set; }
        public int? StandardId { get; set; }
        public char? Approved { get; set; }
        public string? ContractDocuments { get; set; }
        public string? Remarks { get; set; }
        public char? Active { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        //public int? EditedBy { get; set; }
       // public DateTime? EditedDate { get; set; }
       // public int? ApprovedBy { get; set; }
       // public DateTime? ApprovedDate { get; set; }
    }

    public class UpdateLicenseDto
    {
        public int LicenseId { get; set; }
        public DateOnly StartOrRenewalDate { get; set; }
        public int ContractPeriodInMonths { get; set; }
        public DateOnly EndDate { get; set; }
        public int CustomerId { get; set; }
        public int StandardId { get; set; }
         public char? Approved { get; set; }
        public string? ContractDocuments { get; set; }
        public string? Remarks { get; set; }
        public char? Active { get; set; }
       // public int? CreatedBy { get; set; }
       // public DateOnly? CreatedDate { get; set; }
        public int? EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }
        //  public int? ApprovedBy { get; set; }
        // public DateOnly? ApprovedDate { get; set; }
    }
}