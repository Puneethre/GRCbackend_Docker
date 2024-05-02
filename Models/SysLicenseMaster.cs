using System;
using System.Collections.Generic;

namespace GRCServices.Models;

public partial class SysLicenseMaster
{
    public int LicnId { get; set; }

    public DateOnly StartOrRenewalDate { get; set; }

    public int ContractPeriodInMonths { get; set; }

    public DateOnly EndDate { get; set; }

    public int CustomerId { get; set; }

    public int StandardId { get; set; }

    public string? ContractDocuments { get; set; }

    public string? Remarks { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? EditedBy { get; set; }

    public DateTime? EditedDate { get; set; }

    public int? ApprovedBy { get; set; }

    public DateTime? ApprovedDate { get; set; }

    public bool? Approved { get; set; }

    public bool? IsActive { get; set; }

    public virtual SysUserLogin? ApprovedByNavigation { get; set; }

    public virtual SysUserLogin CreatedByNavigation { get; set; } = null!;

    public virtual SysCustomerInfo Customer { get; set; } = null!;

    public virtual SysUserLogin? EditedByNavigation { get; set; }

    public virtual StandardMaster Standard { get; set; } = null!;
}
