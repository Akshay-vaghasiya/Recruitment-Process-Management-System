namespace Backend.Models
{
    public class College
    {
        public int PkCollegeId { get; set; }
        public string? CollegeName { get; set; }
        public string? ContactNo { get; set; }
        public string? Location { get; set; }

        public virtual ICollection<CampusRecruitment> CampusRecruitments { get; set; } = new List<CampusRecruitment>();
    }
}
