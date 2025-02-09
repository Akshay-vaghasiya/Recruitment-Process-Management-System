using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Models;

public partial class InterviewStatus
{
    public int PkInterviewStatusId { get; set; }

    public string? Name { get; set; }

    [JsonIgnore]
    public virtual ICollection<Interview> Interviews { get; set; } = new List<Interview>();
}
