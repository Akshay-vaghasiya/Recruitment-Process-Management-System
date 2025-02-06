using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class InterviewFeedback
{
    public int PkInterviewFeedbackId { get; set; }

    public int? FkInterviewId { get; set; }

    public int? FkInterviewerId { get; set; }

    public int? Rating { get; set; }

    public string? Comments { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Interview? FkInterview { get; set; }

    public virtual User? FkInterviewer { get; set; }
}
