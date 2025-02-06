using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Models;

public partial class User
{
    public int PkUserId { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Password { get; set; }

    public DateOnly? JoiningDate { get; set; }

    public DateOnly? LeavingDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<InterviewFeedback> InterviewFeedbacks { get; set; } = new List<InterviewFeedback>();

    public virtual ICollection<InterviewPanel> InterviewPanels { get; set; } = new List<InterviewPanel>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
