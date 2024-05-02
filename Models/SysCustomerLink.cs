using System;
using System.Collections.Generic;

namespace GRCServices.Models;

public partial class SysCustomerLink
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string DbConStr { get; set; } = null!;

    public int CreatedBy { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public bool IsActive { get; set; }

    public virtual SysUserLogin CreatedByNavigation { get; set; } = null!;

    public virtual SysCustomerInfo Customer { get; set; } = null!;
}
