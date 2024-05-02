using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace GRCServices.Dto_s
{
    public class GetAssignmentMasterDto
    {
        public int Id { get; set; }

        public string? ActivityName { get; set; }

        public string? User { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public int? Doerstatus { get; set; }

        public bool? AuditCheck { get; set; }

        public bool? ApprovalStatus { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public string? Approver { get; set; }
    }

    public class AddAssignmentMasterDto
    {
        public int? ActivityId { get; set; }

        public int? UserId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? Doerstatus { get; set; }

        public char? AuditCheck { get; set; }

        public char? ApprovalStatus { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public int? ApproverId { get; set; }

        public int? CustomerId { get; set; }
    }

    public class UpdateAssignmentMasterDto
    {
        public int ActivityId { get; set; }

        public int UserId { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public int Doerstatus { get; set; }

        public bool AuditCheck { get; set; }

        public bool ApprovalStatus { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public int? ApproverId { get; set; }

        public int? CustomerId { get; set; }
    }

    public class GetAssignmentMasterForUserDto
    {
        public int Id { get; set; }
        public string? ActivityName { get; set; }
        public string? ActivityDescr { get; set; }
        public string? DoerComments { get; set; }
        public string? ApproverComments { get; set; }
        public string? EvidenceDetails { get; set; }
    }

    public class UpdateAssignmentMasterForUserDto
    {
        public int Id { get; set; }
        public string? ActivityName { get; set; }
        public string? ActivityDescr { get; set; }
        public string? DoerComments { get; set; }
        public string? ApproverComments { get; set; }
        public string? EvidenceDetails { get; set; }
    }
}
