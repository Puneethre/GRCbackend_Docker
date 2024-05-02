using System;
using System.Collections.Generic;

namespace GRCServices.Models;

public partial class SysRoleMaster
{
    public int SysRoleId { get; set; }

    public string? RoleName { get; set; }

    public int? RoleTypeId { get; set; }

    public string? Description { get; set; }

    public string? Comments { get; set; }

    public int? CreatedBy { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreateDateTime { get; set; }

    public int? SysRoleTypeId { get; set; }

    public virtual SysUserLogin? CreatedByNavigation { get; set; }

    public virtual RoleType? RoleType { get; set; }

    public virtual SysRoleType? SysRoleType { get; set; }

    public virtual ICollection<SysUserLogin> SysUserLogins { get; set; } = new List<SysUserLogin>();
}
