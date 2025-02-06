using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Report
{
    public int PkReportId { get; set; }

    public int? FkReportTypeId { get; set; }

    public int? FkGeneratedBy { get; set; }

    public string? ReportData { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User? FkGeneratedByNavigation { get; set; }

    public virtual ReportType? FkReportType { get; set; }
}
