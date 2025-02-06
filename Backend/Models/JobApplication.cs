using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class JobApplication
{
    public int PkJobApplicationId { get; set; }

    public int? FkJobPositionId { get; set; }

    public int? FkCandidateId { get; set; }

    public int? FkStatusId { get; set; }

    public virtual Candidate? FkCandidate { get; set; }

    public virtual JobPosition? FkJobPosition { get; set; }

    public virtual ApplicationStatus? FkStatus { get; set; }
}
