using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Document
{
    public int PkDocumentId { get; set; }

    public int? FkCandidateId { get; set; }

    public int? FkDocumentTypeId { get; set; }

    public string? DocumentUrl { get; set; }

    public int? FkStatusId { get; set; }

    public virtual Candidate? FkCandidate { get; set; }

    public virtual DocumentType? FkDocumentType { get; set; }

    public virtual DocumentStatus? FkStatus { get; set; }
}
