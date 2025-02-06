using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class InterviewStatus
{
    public int PkInterviewStatusId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Interview> Interviews { get; set; } = new List<Interview>();
}
