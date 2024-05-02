using System;
using System.Collections.Generic;

namespace GRCServices.Models;

public partial class CategoryMaster
{
    public int CategoryId { get; set; }

    public string Category { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<CategoryListMaster> CategoryListMasters { get; set; } = new List<CategoryListMaster>();
}
