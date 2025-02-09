using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Models;

public partial class ApplicationStatus
{
    public int PkApplicationStatusId { get; set; }

    public string? Name { get; set; }

    [JsonIgnore]
    public virtual ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
}
