namespace Backend.Models
{
    public class CampusRecruitment
    {
        public int PkCampusRecruitmentId { get; set; }
        public string? EventName { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public int? FkCollegeId { get; set; }
        public virtual College FkCollege { get; set; }
        public virtual ICollection<ResumeReview> ResumeReviews { get; set; } = new HashSet<ResumeReview>();
        public virtual ICollection<Interview> Interviews { get; set; } = new HashSet<Interview>();
    }
}
