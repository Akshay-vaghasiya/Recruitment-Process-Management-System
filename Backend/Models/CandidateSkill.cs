using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class CandidateSkill
{
    public int PkCandidateSkillId { get; set; }

    public int? FkCandidateId { get; set; }

    public int? FkSkillId { get; set; }

    public int? YearsOfExperience { get; set; }

    public virtual Candidate? FkCandidate { get; set; }

    public virtual Skill? FkSkill { get; set; }
}
