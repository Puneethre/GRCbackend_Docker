using System;
using System.Collections.Generic;

namespace ConsoleApp1.Models;

public partial class StandardMaster
{
    public int Id { get; set; }

    public string? ShortName { get; set; }

    public string Name { get; set; } = null!;

    public int GovrId { get; set; }

    public int Levels { get; set; }

    public string LevelNames { get; set; } = null!;

    public int? NoOfControls { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<ComplianceMaster> ComplianceMasters { get; set; } = new List<ComplianceMaster>();

    public virtual GovernanceMaster Govr { get; set; } = null!;
}
