using System;
using System.Collections.Generic;

namespace GRCServices.Models;

public partial class DomainMaster
{
    public int Id { get; set; }

    public string DomainName { get; set; } = null!;
}
