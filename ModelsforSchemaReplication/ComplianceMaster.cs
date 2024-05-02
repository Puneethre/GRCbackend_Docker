using System;
using System.Collections.Generic;

namespace ConsoleApp1.Models;

public partial class ComplianceMaster
{
    public int Id { get; set; }

    public int StandardId { get; set; }

    public int GovernanceId { get; set; }

    public DateOnly ComplStartDate { get; set; }

    public DateOnly ComplEndDate { get; set; }

    public bool MetCompliance { get; set; }

    public bool IsActive { get; set; }

    public virtual GovernanceMaster Governance { get; set; } = null!;

    public virtual StandardMaster Standard { get; set; } = null!;
}
