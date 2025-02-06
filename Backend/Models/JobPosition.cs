using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Models;

public partial class JobPosition
{
    public int PkJobPositionId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public int? FkStatusId { get; set; }

    public string? ClosureReason { get; set; }

    public int? FkSelectedCandidateId { get; set; }

    public int? FkReviewerId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual JobStatus? FkStatus { get; set; }

    public virtual ICollection<Interview> Interviews { get; set; } = new List<Interview>();

    public virtual ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();

    public virtual ICollection<JobSkill> JobSkills { get; set; } = new List<JobSkill>();

    public virtual ICollection<ResumeReview> ResumeReviews { get; set; } = new List<ResumeReview>();
}
