namespace Backend.Models
{
    public class CampusHiringCandidate
    {
        public int PkCampusHiringCandidateId { get; set; }
        public int? FkCandidateId { get; set; }
        public int? FkCampusRecruitmentId { get; set; }
        public virtual Candidate? FkCandidate { get; set; }
        public virtual CampusRecruitment? FkCampusRecruitment { get; set; }
    }
}
