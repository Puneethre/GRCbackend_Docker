using System;
using System.Collections.Generic;

namespace ConsoleApp1.Models;

public partial class ClientRoleMaster
{
    public int CliRoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public int? RoleTypeId { get; set; }

    public string? Description { get; set; }

    public string? Comments { get; set; }

    public int? CreatedBy { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public virtual ICollection<ActivityMaster> ActivityMasterApproverRoleNavigations { get; set; } = new List<ActivityMaster>();

    public virtual ICollection<ActivityMaster> ActivityMasterDoerRoleNavigations { get; set; } = new List<ActivityMaster>();

    public virtual ICollection<ClientUserInfo> ClientUserInfos { get; set; } = new List<ClientUserInfo>();

    public virtual RoleType? RoleType { get; set; }
}
