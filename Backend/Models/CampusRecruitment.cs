namespace Backend.Models
{
    public class CampusRecruitment
    {
        public int PkCampusRecruitmentId { get; set; }
        public string? EventName { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public int? FkCollegeId { get; set; }
        public virtual College FkCollege { get; set; }
        public virtual ICollection<CampusHiringCandidate> HiringCandidates { get; set; } = new HashSet<CampusHiringCandidate>();
    }
}
