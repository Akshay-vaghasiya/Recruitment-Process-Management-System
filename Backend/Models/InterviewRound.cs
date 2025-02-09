using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Models;

public partial class InterviewRound
{
    public int PkInterviewRoundId { get; set; }

    public string? Name { get; set; }

    [JsonIgnore]
    public virtual ICollection<Interview> Interviews { get; set; } = new List<Interview>();
}
