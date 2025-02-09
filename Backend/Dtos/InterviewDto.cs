namespace Backend.Dtos
{
    public class InterviewDto
    {
        public int? FkCandidateId { get; set; }

        public int? FkJobPositionId { get; set; }

        public int? FkInterviewRoundId { get; set; }

        public int? RoundNumber { get; set; }

        public DateTime? ScheduledTime { get; set; }

        public int? FkStatusId { get; set; }
    }
}
