using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class InterviewRound
{
    public int PkInterviewRoundId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Interview> Interviews { get; set; } = new List<Interview>();
}
