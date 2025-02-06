using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Models;

public partial class Skill
{
    public int PkSkillId { get; set; }

    public string? Name { get; set; }

    [JsonIgnore]
    public virtual ICollection<CandidateSkill> CandidateSkills { get; set; } = new List<CandidateSkill>();
    [JsonIgnore]
    public virtual ICollection<JobSkill> JobSkills { get; set; } = new List<JobSkill>();
}
