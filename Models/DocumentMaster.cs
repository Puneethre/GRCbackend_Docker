using System;
using System.Collections.Generic;

namespace GRCServices.Models;

public partial class DocumentMaster
{
    public int Id { get; set; }

    public string DocumentName { get; set; } = null!;

    public virtual ICollection<ActivityMaster> ActivityMasterOutputDocumentPathNavigations { get; set; } = new List<ActivityMaster>();

    public virtual ICollection<ActivityMaster> ActivityMasterRefDocuments { get; set; } = new List<ActivityMaster>();
}
