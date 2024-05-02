using System;
using System.Collections.Generic;

namespace GRCServices.Models;

public partial class ActivitiyNameMaster
{
    public int Id { get; set; }

    public string ActivityName { get; set; } = null!;

    public virtual ICollection<ActivityMaster> ActivityMasterActivityNames { get; set; } = new List<ActivityMaster>();

    public virtual ICollection<ActivityMaster> ActivityMasterTriggeringActivityNames { get; set; } = new List<ActivityMaster>();
}
