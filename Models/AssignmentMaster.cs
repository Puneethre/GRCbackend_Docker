using System;
using System.Collections.Generic;

namespace GRCServices.Models;

public partial class AssignmentMaster
{
    public int Id { get; set; }

    public int ActivityMasterId { get; set; }

    public int DoerCliUserId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public DateTime? ApprovalDate { get; set; }

    public int? ApproverCliUserId { get; set; }

    public string? DoerComments { get; set; }

    public string? ApproverComments { get; set; }

    public string? EvidenceDetails { get; set; }

    public bool AuditCheck { get; set; }

    public bool ApprovalStatus { get; set; }

    public int DoerStatus { get; set; }

    public virtual ActivityMaster ActivityMaster { get; set; } = null!;

    public virtual ClientUserInfo? ApproverCliUser { get; set; }

    public virtual ClientUserInfo DoerCliUser { get; set; } = null!;
}
