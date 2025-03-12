using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Models;

public partial class ResumeReview
{
    public int PkResumeReviewId { get; set; }

    public int? FkCandidateId { get; set; }

    public int? FkJobPositionId { get; set; }

    public int? FkCampusRecruitmentId { get; set; }

    public string? Comments { get; set; }

    public DateTime? CreatedAt { get; set; }

    [JsonIgnore]
    public virtual Candidate? FkCandidate { get; set; }

    [JsonIgnore]
    public virtual JobPosition? FkJobPosition { get; set; }

    public virtual CampusRecruitment? FkCampusRecruitment { get; set; }
}
