using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

public partial class RecruitmentProcessManagementSystemContext : DbContext
{
    public RecruitmentProcessManagementSystemContext()
    {
    }

    public RecruitmentProcessManagementSystemContext(DbContextOptions<RecruitmentProcessManagementSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ApplicationStatus> ApplicationStatuses { get; set; }

    public virtual DbSet<Candidate> Candidates { get; set; }

    public virtual DbSet<CandidateSkill> CandidateSkills { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<DocumentStatus> DocumentStatuses { get; set; }

    public virtual DbSet<DocumentType> DocumentTypes { get; set; }

    public virtual DbSet<Interview> Interviews { get; set; }

    public virtual DbSet<InterviewFeedback> InterviewFeedbacks { get; set; }

    public virtual DbSet<InterviewPanel> InterviewPanels { get; set; }

    public virtual DbSet<InterviewRound> InterviewRounds { get; set; }

    public virtual DbSet<InterviewStatus> InterviewStatuses { get; set; }

    public virtual DbSet<JobApplication> JobApplications { get; set; }

    public virtual DbSet<JobPosition> JobPositions { get; set; }

    public virtual DbSet<JobSkill> JobSkills { get; set; }

    public virtual DbSet<JobStatus> JobStatuses { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<ReportType> ReportTypes { get; set; }

    public virtual DbSet<ResumeReview> ResumeReviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=L10-VAGHAAKS-1\\SQLEXPRESS;Initial Catalog=Recruitment_Process_Management_System; Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationStatus>(entity =>
        {
            entity.HasKey(e => e.PkApplicationStatusId).HasName("PK__applicat__1B80B3DA4A1806EA");

            entity.ToTable("application_status");

            entity.Property(e => e.PkApplicationStatusId).HasColumnName("pk_application_status_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Candidate>(entity =>
        {
            entity.HasKey(e => e.PkCandidateId).HasName("PK__candidat__D43BFB2A752C6A10");

            entity.ToTable("candidate");

            entity.HasIndex(e => e.Email, "UQ__candidat__AB6E61649995AF39").IsUnique();

            entity.Property(e => e.PkCandidateId).HasColumnName("pk_candidate_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("full_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.ResumeUrl)
                .HasColumnType("text")
                .HasColumnName("resume_url");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.YearsOfExperience).HasColumnName("years_of_experience");

        });

        modelBuilder.Entity<CandidateSkill>(entity =>
        {
            entity.HasKey(e => e.PkCandidateSkillId).HasName("PK__candidat__634633AC0E7AA1B1");

            entity.ToTable("candidate_skill");

            entity.Property(e => e.PkCandidateSkillId).HasColumnName("pk_candidate_skill_id");
            entity.Property(e => e.FkCandidateId).HasColumnName("fk_candidate_id");
            entity.Property(e => e.FkSkillId).HasColumnName("fk_skill_id");
            entity.Property(e => e.YearsOfExperience).HasColumnName("years_of_experience");

            entity.HasOne(d => d.FkCandidate).WithMany(p => p.CandidateSkills)
                .HasForeignKey(d => d.FkCandidateId)
                .HasConstraintName("FK__candidate__fk_ca__3F466844");

            entity.HasOne(d => d.FkSkill).WithMany(p => p.CandidateSkills)
                .HasForeignKey(d => d.FkSkillId)
                .HasConstraintName("FK__candidate__fk_sk__403A8C7D");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.PkDocumentId).HasName("PK__document__0467181A1D43A66E");

            entity.ToTable("document");

            entity.Property(e => e.PkDocumentId).HasColumnName("pk_document_id");
            entity.Property(e => e.DocumentUrl)
                .HasColumnType("text")
                .HasColumnName("document_url");
            entity.Property(e => e.FkCandidateId).HasColumnName("fk_candidate_id");
            entity.Property(e => e.FkDocumentTypeId).HasColumnName("fk_document_type_id");
            entity.Property(e => e.FkStatusId).HasColumnName("fk_status_id");

            entity.HasOne(d => d.FkCandidate).WithMany(p => p.Documents)
                .HasForeignKey(d => d.FkCandidateId)
                .HasConstraintName("FK__document__fk_can__5629CD9C");

            entity.HasOne(d => d.FkDocumentType).WithMany(p => p.Documents)
                .HasForeignKey(d => d.FkDocumentTypeId)
                .HasConstraintName("FK__document__fk_doc__571DF1D5");

            entity.HasOne(d => d.FkStatus).WithMany(p => p.Documents)
                .HasForeignKey(d => d.FkStatusId)
                .HasConstraintName("FK__document__fk_sta__5812160E");
        });

        modelBuilder.Entity<DocumentStatus>(entity =>
        {
            entity.HasKey(e => e.PkDocumentStatusId).HasName("PK__document__2BFB62BD36F8EBB1");

            entity.ToTable("document_status");

            entity.Property(e => e.PkDocumentStatusId).HasColumnName("pk_document_status_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<DocumentType>(entity =>
        {
            entity.HasKey(e => e.PkDocumentTypeId).HasName("PK__document__123FA8468734685F");

            entity.ToTable("document_type");

            entity.Property(e => e.PkDocumentTypeId).HasColumnName("pk_document_type_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Interview>(entity =>
        {
            entity.HasKey(e => e.PkInterviewId).HasName("PK__intervie__4EF41A8FC3728FA5");

            entity.ToTable("interview");

            entity.Property(e => e.PkInterviewId).HasColumnName("pk_interview_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.FkCandidateId).HasColumnName("fk_candidate_id");
            entity.Property(e => e.FkInterviewRoundId).HasColumnName("fk_interview_round_id");
            entity.Property(e => e.FkJobPositionId).HasColumnName("fk_job_position_id");
            entity.Property(e => e.FkStatusId).HasColumnName("fk_status_id");
            entity.Property(e => e.RoundNumber).HasColumnName("round_number");
            entity.Property(e => e.ScheduledTime)
                .HasColumnType("datetime")
                .HasColumnName("scheduled_time");

            entity.HasOne(d => d.FkCandidate).WithMany(p => p.Interviews)
                .HasForeignKey(d => d.FkCandidateId)
                .HasConstraintName("FK__interview__fk_ca__4BAC3F29");

            entity.HasOne(d => d.FkInterviewRound).WithMany(p => p.Interviews)
                .HasForeignKey(d => d.FkInterviewRoundId)
                .HasConstraintName("FK__interview__fk_in__4D94879B");

            entity.HasOne(d => d.FkJobPosition).WithMany(p => p.Interviews)
                .HasForeignKey(d => d.FkJobPositionId)
                .HasConstraintName("FK__interview__fk_jo__4CA06362");

            entity.HasOne(d => d.FkStatus).WithMany(p => p.Interviews)
                .HasForeignKey(d => d.FkStatusId)
                .HasConstraintName("FK__interview__fk_st__4E88ABD4");
        });

        modelBuilder.Entity<InterviewFeedback>(entity =>
        {
            entity.HasKey(e => e.PkInterviewFeedbackId).HasName("PK__intervie__4139B8F6915BB39C");

            entity.ToTable("interview_feedback");

            entity.Property(e => e.PkInterviewFeedbackId).HasColumnName("pk_interview_feedback_id");
            entity.Property(e => e.Comments)
                .HasColumnType("text")
                .HasColumnName("comments");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.FkInterviewId).HasColumnName("fk_interview_id");
            entity.Property(e => e.FkInterviewerId).HasColumnName("fk_interviewer_id");
            entity.Property(e => e.Rating).HasColumnName("rating");

            entity.HasOne(d => d.FkInterview).WithMany(p => p.InterviewFeedbacks)
                .HasForeignKey(d => d.FkInterviewId)
                .HasConstraintName("FK__interview__fk_in__68487DD7");

            entity.HasOne(d => d.FkInterviewer).WithMany(p => p.InterviewFeedbacks)
                .HasForeignKey(d => d.FkInterviewerId)
                .HasConstraintName("FK__interview__fk_in__693CA210");
        });

        modelBuilder.Entity<InterviewPanel>(entity =>
        {
            entity.HasKey(e => e.PkInterviewPanelId).HasName("PK__intervie__1295035212BE1CB7");

            entity.ToTable("interview_panel");

            entity.Property(e => e.PkInterviewPanelId).HasColumnName("pk_interview_panel_id");
            entity.Property(e => e.FkInterviewId).HasColumnName("fk_interview_id");
            entity.Property(e => e.FkInterviewerId).HasColumnName("fk_interviewer_id");

            entity.HasOne(d => d.FkInterview).WithMany(p => p.InterviewPanels)
                .HasForeignKey(d => d.FkInterviewId)
                .HasConstraintName("FK__interview__fk_in__6477ECF3");

            entity.HasOne(d => d.FkInterviewer).WithMany(p => p.InterviewPanels)
                .HasForeignKey(d => d.FkInterviewerId)
                .HasConstraintName("FK__interview__fk_in__656C112C");
        });

        modelBuilder.Entity<InterviewRound>(entity =>
        {
            entity.HasKey(e => e.PkInterviewRoundId).HasName("PK__intervie__FF56A1D577728A50");

            entity.ToTable("interview_round");

            entity.Property(e => e.PkInterviewRoundId).HasColumnName("pk_interview_round_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<InterviewStatus>(entity =>
        {
            entity.HasKey(e => e.PkInterviewStatusId).HasName("PK__intervie__247A960E45B6F140");

            entity.ToTable("interview_status");

            entity.Property(e => e.PkInterviewStatusId).HasColumnName("pk_interview_status_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<JobApplication>(entity =>
        {
            entity.HasKey(e => e.PkJobApplicationId).HasName("PK__job_appl__DB67DEC16367F743");

            entity.ToTable("job_applications");

            entity.Property(e => e.PkJobApplicationId).HasColumnName("pk_job_application_id");
            entity.Property(e => e.FkCandidateId).HasColumnName("fk_candidate_id");
            entity.Property(e => e.FkJobPositionId).HasColumnName("fk_job_position_id");
            entity.Property(e => e.FkStatusId).HasColumnName("fk_status_id");

            entity.HasOne(d => d.FkCandidate).WithMany(p => p.JobApplications)
                .HasForeignKey(d => d.FkCandidateId)
                .HasConstraintName("FK__job_appli__fk_ca__3B75D760");

            entity.HasOne(d => d.FkJobPosition).WithMany(p => p.JobApplications)
                .HasForeignKey(d => d.FkJobPositionId)
                .HasConstraintName("FK__job_appli__fk_jo__3A81B327");

            entity.HasOne(d => d.FkStatus).WithMany(p => p.JobApplications)
                .HasForeignKey(d => d.FkStatusId)
                .HasConstraintName("FK__job_appli__fk_st__3C69FB99");
        });

        modelBuilder.Entity<JobPosition>(entity =>
        {
            entity.HasKey(e => e.PkJobPositionId).HasName("PK__job_posi__839B68E8D3381D1A");

            entity.ToTable("job_position");

            entity.Property(e => e.PkJobPositionId).HasColumnName("pk_job_position_id");
            entity.Property(e => e.ClosureReason)
                .HasColumnType("text")
                .HasColumnName("closure_reason");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.FkReviewerId).HasColumnName("fk_reviewer_id");
            entity.Property(e => e.FkSelectedCandidateId).HasColumnName("fk_selected_candidate_id");
            entity.Property(e => e.FkStatusId).HasColumnName("fk_status_id");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.FkStatus).WithMany(p => p.JobPositions)
                .HasForeignKey(d => d.FkStatusId)
                .HasConstraintName("FK__job_posit__fk_st__267ABA7A");
        });

        modelBuilder.Entity<JobSkill>(entity =>
        {
            entity.HasKey(e => e.PkJobSkillId).HasName("PK__job_skil__B60C9BF6D647915D");

            entity.ToTable("job_skill");

            entity.Property(e => e.PkJobSkillId).HasColumnName("pk_job_skill_id");
            entity.Property(e => e.FkJobPositionId).HasColumnName("fk_job_position_id");
            entity.Property(e => e.FkSkillId).HasColumnName("fk_skill_id");
            entity.Property(e => e.IsRequired).HasColumnName("is_required");

            entity.HasOne(d => d.FkJobPosition).WithMany(p => p.JobSkills)
                .HasForeignKey(d => d.FkJobPositionId)
                .HasConstraintName("FK__job_skill__fk_jo__2D27B809");

            entity.HasOne(d => d.FkSkill).WithMany(p => p.JobSkills)
                .HasForeignKey(d => d.FkSkillId)
                .HasConstraintName("FK__job_skill__fk_sk__2E1BDC42");
        });

        modelBuilder.Entity<JobStatus>(entity =>
        {
            entity.HasKey(e => e.PkJobStatusId).HasName("PK__job_stat__0C7DACDF736C5695");

            entity.ToTable("job_status");

            entity.Property(e => e.PkJobStatusId).HasColumnName("pk_job_status_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.PkNotificationId).HasName("PK__notifica__E2D15614C8AC6AD6");

            entity.ToTable("notification");

            entity.Property(e => e.PkNotificationId).HasColumnName("pk_notification_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.FkUserId).HasColumnName("fk_user_id");
            entity.Property(e => e.IsRead).HasColumnName("is_read");
            entity.Property(e => e.Message)
                .HasColumnType("text")
                .HasColumnName("message");

            entity.HasOne(d => d.FkUser).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.FkUserId)
                .HasConstraintName("FK__notificat__fk_us__6D0D32F4");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.PkReportId).HasName("PK__report__28BD8B5C92A88C60");

            entity.ToTable("report");

            entity.Property(e => e.PkReportId).HasColumnName("pk_report_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.FkGeneratedBy).HasColumnName("fk_generated_by");
            entity.Property(e => e.FkReportTypeId).HasColumnName("fk_report_type_id");
            entity.Property(e => e.ReportData)
                .HasColumnType("text")
                .HasColumnName("report_data");

            entity.HasOne(d => d.FkGeneratedByNavigation).WithMany(p => p.Reports)
                .HasForeignKey(d => d.FkGeneratedBy)
                .HasConstraintName("FK__report__fk_gener__73BA3083");

            entity.HasOne(d => d.FkReportType).WithMany(p => p.Reports)
                .HasForeignKey(d => d.FkReportTypeId)
                .HasConstraintName("FK__report__fk_repor__72C60C4A");
        });

        modelBuilder.Entity<ReportType>(entity =>
        {
            entity.HasKey(e => e.PkReportTypeId).HasName("PK__report_t__852F07E0D9CAB15F");

            entity.ToTable("report_type");

            entity.Property(e => e.PkReportTypeId).HasColumnName("pk_report_type_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ResumeReview>(entity =>
        {
            entity.HasKey(e => e.PkResumeReviewId).HasName("PK__resume_r__CA50FD1519F29FA9");

            entity.ToTable("resume_review");

            entity.Property(e => e.PkResumeReviewId).HasColumnName("pk_resume_review_id");
            entity.Property(e => e.Comments)
                .HasColumnType("text")
                .HasColumnName("comments");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.FkCandidateId).HasColumnName("fk_candidate_id");
            entity.Property(e => e.FkJobPositionId).HasColumnName("fk_job_position_id");

            entity.HasOne(d => d.FkCandidate).WithMany(p => p.ResumeReviews)
                .HasForeignKey(d => d.FkCandidateId)
                .HasConstraintName("FK__resume_re__fk_ca__4316F928");

            entity.HasOne(d => d.FkJobPosition).WithMany(p => p.ResumeReviews)
                .HasForeignKey(d => d.FkJobPositionId)
                .HasConstraintName("FK__resume_re__fk_jo__440B1D61");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.PkRoleId).HasName("PK__roles__12E769BBED1D1DA1");

            entity.ToTable("roles");

            entity.Property(e => e.PkRoleId).HasColumnName("pk_role_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.PkSkillId).HasName("PK__skill__BE38287969434363");

            entity.ToTable("skill");

            entity.Property(e => e.PkSkillId).HasColumnName("pk_skill_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.PkUserId).HasName("PK__user__2F416313F3F7F9F4");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "UQ__user__AB6E6164410F3DC6").IsUnique();

            entity.Property(e => e.PkUserId).HasColumnName("pk_user_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("full_name");
            entity.Property(e => e.JoiningDate).HasColumnName("joining_date");
            entity.Property(e => e.LeavingDate).HasColumnName("leaving_date");
            entity.Property(e => e.Password)
                .HasColumnType("text")
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("phone");

        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.PkUserRoleId).HasName("PK__user_rol__4BA152D8B279EAD4");

            entity.ToTable("user_role");

            entity.Property(e => e.PkUserRoleId).HasColumnName("pk_user_role_id");
            entity.Property(e => e.FkRoleId).HasColumnName("fk_role_id");
            entity.Property(e => e.FkUserId).HasColumnName("fk_user_id");

            entity.HasOne(d => d.FkRole).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.FkRoleId)
                .HasConstraintName("FK__user_role__fk_ro__619B8048");

            entity.HasOne(d => d.FkUser).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.FkUserId)
                .HasConstraintName("FK__user_role__fk_us__60A75C0F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
