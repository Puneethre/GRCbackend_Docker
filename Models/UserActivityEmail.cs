using System;
using System.Collections.Generic;

namespace GRCServices.Models;

public partial class UserActivityEmail
{
    public int Id { get; set; }

    public int ActivityId { get; set; }

    public string EmailCodeToActivity { get; set; } = null!;

    public virtual ActivityMaster Activity { get; set; } = null!;
}
