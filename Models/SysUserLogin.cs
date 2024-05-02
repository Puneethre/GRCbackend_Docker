using System;
using System.Collections.Generic;

namespace GRCServices.Models;

public partial class SysUserLogin
{
    public int SysUserId { get; set; }

    public string LoginEmailId { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Mfacode { get; set; }

    public DateTime? LastloginDatetime { get; set; }

    public int LoginAttemptsAllowed { get; set; }

    public int? SysRoleId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedDatetime { get; set; }

    public int? CustomerId { get; set; }

    public Guid? Guid { get; set; }

    public bool IsActive { get; set; }

    public bool NewUser { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<ClientRoleMaster> ClientRoleMasters { get; set; } = new List<ClientRoleMaster>();

    public virtual SysUserLogin CreatedByNavigation { get; set; } = null!;

    public virtual SysCustomerInfo Customer { get; set; } = null!;

    public virtual ICollection<SysUserLogin> InverseCreatedByNavigation { get; set; } = new List<SysUserLogin>();

    public virtual ICollection<SysCustomerInfo> SysCustomerInfos { get; set; } = new List<SysCustomerInfo>();

    public virtual ICollection<SysCustomerLink> SysCustomerLinks { get; set; } = new List<SysCustomerLink>();

    public virtual ICollection<SysLicenseMaster> SysLicenseMasterApprovedByNavigations { get; set; } = new List<SysLicenseMaster>();

    public virtual ICollection<SysLicenseMaster> SysLicenseMasterCreatedByNavigations { get; set; } = new List<SysLicenseMaster>();

    public virtual ICollection<SysLicenseMaster> SysLicenseMasterEditedByNavigations { get; set; } = new List<SysLicenseMaster>();

    public virtual SysRoleMaster SysRole { get; set; } = null!;

    public virtual ICollection<SysRoleMaster> SysRoleMasters { get; set; } = new List<SysRoleMaster>();

    public virtual ICollection<SysCustomerInfo> Customers { get; set; } = new List<SysCustomerInfo>();
}
