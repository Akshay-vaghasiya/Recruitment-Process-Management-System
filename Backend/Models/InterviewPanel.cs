using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class InterviewPanel
{
    public int PkInterviewPanelId { get; set; }

    public int? FkInterviewId { get; set; }

    public int? FkInterviewerId { get; set; }

    public virtual Interview? FkInterview { get; set; }

    public virtual User? FkInterviewer { get; set; }
}
