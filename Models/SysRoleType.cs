using System;
using System.Collections.Generic;

namespace GRCServices.Models;

public partial class SysRoleType
{
    public int Id { get; set; }

    public string RoleTypeDesc { get; set; } = null!;

    public virtual ICollection<SysRoleMaster> SysRoleMasters { get; set; } = new List<SysRoleMaster>();
}
