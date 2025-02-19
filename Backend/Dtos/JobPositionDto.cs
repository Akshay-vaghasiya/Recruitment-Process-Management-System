namespace Backend.Dtos
{
    public class JobPositionDto
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public int? FkStatusId { get; set; }

        public int? FkReviewerId { get; set; }

        public List<int>? RequireSkills {  get; set; }

        public List<int>? Skills { get; set; }

        public string? ClosureReason { get; set; }

        public int? FkSelectedCandidateId { get; set; }

        public DateOnly? JoiningDate { get; set; }
    }
}
