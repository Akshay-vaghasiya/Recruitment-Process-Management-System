using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class DocumentStatus
{
    public int PkDocumentStatusId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
}
