using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Models;

public partial class JobStatus
{
    public int PkJobStatusId { get; set; }

    public string? Name { get; set; }

    [JsonIgnore]
    public virtual ICollection<JobPosition> JobPositions { get; set; } = new List<JobPosition>();
}
