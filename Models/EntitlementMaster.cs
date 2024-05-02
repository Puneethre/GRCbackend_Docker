using System;
using System.Collections.Generic;

namespace GRCServices.Models;

public partial class EntitlementMaster
{
    public int RoleId { get; set; }

    public string MenuItem { get; set; } = null!;

    public bool IsActive { get; set; }
}
