﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;

namespace Backend.Models;

public partial class Candidate
{
    public int PkCandidateId { get; set; }

    public string? FullName { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? ResumeUrl { get; set; }

    public int? YearsOfExperience { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<CandidateSkill> CandidateSkills { get; set; } = new List<CandidateSkill>();

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual ICollection<Interview> Interviews { get; set; } = new List<Interview>();

    public virtual ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();

    public virtual ICollection<ResumeReview> ResumeReviews { get; set; } = new List<ResumeReview>();

    public virtual ICollection<CandidateNotification> CandidateNotifications { get; set; } = new List<CandidateNotification>();

}
