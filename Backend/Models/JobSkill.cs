using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Models;

public partial class JobSkill
{
    public int PkJobSkillId { get; set; }

    public int? FkJobPositionId { get; set; }

    public int? FkSkillId { get; set; }

    public bool? IsRequired { get; set; }

    [JsonIgnore]
    public virtual JobPosition? FkJobPosition { get; set; }

    public virtual Skill? FkSkill { get; set; }
}
