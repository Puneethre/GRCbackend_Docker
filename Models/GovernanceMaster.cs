using System;
using System.Collections.Generic;

namespace GRCServices.Models;

public partial class GovernanceMaster
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? ShortName { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<ComplianceMaster> ComplianceMasters { get; set; } = new List<ComplianceMaster>();

    public virtual ICollection<StandardMaster> StandardMasters { get; set; } = new List<StandardMaster>();
}
