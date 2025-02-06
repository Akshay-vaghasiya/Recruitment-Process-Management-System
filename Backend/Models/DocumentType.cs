using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class DocumentType
{
    public int PkDocumentTypeId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
}
