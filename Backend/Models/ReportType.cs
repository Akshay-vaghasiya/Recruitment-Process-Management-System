using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Models;

public partial class ReportType
{
    public int PkReportTypeId { get; set; }

    public string? Name { get; set; }

    [JsonIgnore]
    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}
