using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class ResumeReview
{
    public int PkResumeReviewId { get; set; }

    public int? FkCandidateId { get; set; }

    public int? FkJobPositionId { get; set; }

    public string? Comments { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Candidate? FkCandidate { get; set; }

    public virtual JobPosition? FkJobPosition { get; set; }
}
