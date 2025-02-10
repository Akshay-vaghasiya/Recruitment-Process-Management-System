namespace Backend.Models
{
    public class CandidateNotification
    {
        public int PkNotificationId { get; set; }

        public int? FkCandidateId { get; set; }

        public string? Message { get; set; }

        public bool? IsRead { get; set; }

        public DateTime? CreatedAt { get; set; }

        public virtual Candidate? FkCandidate { get; set; }
    }
}
