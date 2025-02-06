using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class ApplicationStatus
{
    public int PkApplicationStatusId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
}
