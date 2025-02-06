using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Interview
{
    public int PkInterviewId { get; set; }

    public int? FkCandidateId { get; set; }

    public int? FkJobPositionId { get; set; }

    public int? FkInterviewRoundId { get; set; }

    public int? RoundNumber { get; set; }

    public DateTime? ScheduledTime { get; set; }

    public int? FkStatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Candidate? FkCandidate { get; set; }

    public virtual InterviewRound? FkInterviewRound { get; set; }

    public virtual JobPosition? FkJobPosition { get; set; }

    public virtual InterviewStatus? FkStatus { get; set; }

    public virtual ICollection<InterviewFeedback> InterviewFeedbacks { get; set; } = new List<InterviewFeedback>();

    public virtual ICollection<InterviewPanel> InterviewPanels { get; set; } = new List<InterviewPanel>();
}
