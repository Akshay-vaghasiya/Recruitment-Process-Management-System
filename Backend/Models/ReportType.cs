using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class ReportType
{
    public int PkReportTypeId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}
