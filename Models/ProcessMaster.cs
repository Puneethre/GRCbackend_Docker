using System;
using System.Collections.Generic;

namespace GRCServices.Models;

public partial class ProcessMaster
{
    public int Id { get; set; }

    public string ProcessName { get; set; } = null!;
}
