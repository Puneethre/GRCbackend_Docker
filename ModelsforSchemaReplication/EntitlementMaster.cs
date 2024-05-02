using System;
using System.Collections.Generic;

namespace ConsoleApp1.Models;

public partial class EntitlementMaster
{
    public int RoleId { get; set; }

    public string MenuItem { get; set; } = null!;

    public bool IsActive { get; set; }
}
