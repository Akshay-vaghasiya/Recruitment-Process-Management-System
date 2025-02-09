using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Models;

public partial class CandidateSkill
{
    public int PkCandidateSkillId { get; set; }

    public int? FkCandidateId { get; set; }

    public int? FkSkillId { get; set; }

    public int? YearsOfExperience { get; set; }

    [JsonIgnore]
    public virtual Candidate? FkCandidate { get; set; }

    public virtual Skill? FkSkill { get; set; }
}
