using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Models;

public partial class Report
{
    public int PkReportId { get; set; }

    public int? FkReportTypeId { get; set; }

    public int? FkGeneratedBy { get; set; }

    public string? ReportData { get; set; }

    public DateTime? CreatedAt { get; set; }

    [JsonIgnore]
    public virtual User? FkGeneratedByNavigation { get; set; }

    [JsonIgnore]
    public virtual ReportType? FkReportType { get; set; }
}
