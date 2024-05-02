using System;
using System.Collections.Generic;

namespace GRCServices.Models;

public partial class StatusMaster
{
    public int Id { get; set; }

    public string Status { get; set; } = null!;
}
