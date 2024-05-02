﻿using System;
using System.Collections.Generic;

namespace GRCServices.Models;

public partial class FrequencyMaster
{
    public int Id { get; set; }

    public string Frequency { get; set; } = null!;

    public virtual ICollection<ActivityMaster> ActivityMasters { get; set; } = new List<ActivityMaster>();
}
