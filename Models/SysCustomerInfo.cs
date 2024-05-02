using System;
using System.Collections.Generic;

namespace GRCServices.Models;

public partial class SysCustomerInfo
{
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string? Street { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string Country { get; set; } = null!;

    public string ContactName { get; set; } = null!;

    public string ContactEmail { get; set; } = null!;

    public string? Description { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public bool IsActive { get; set; }

    public string? ZipCode { get; set; }

    public string ContactPhone { get; set; } = null!;

    public virtual SysUserLogin CreatedByNavigation { get; set; } = null!;

    public virtual SysCustomerLink? SysCustomerLink { get; set; }

    public virtual ICollection<SysLicenseMaster> SysLicenseMasters { get; set; } = new List<SysLicenseMaster>();

    public virtual ICollection<SysUserLogin> SysUserLogins { get; set; } = new List<SysUserLogin>();

    public virtual ICollection<SysUserLogin> SysUsers { get; set; } = new List<SysUserLogin>();
}
