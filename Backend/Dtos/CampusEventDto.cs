namespace Backend.Dtos
{
    public class CampusEventDto
    {
        public string? EventName { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public string? CollegeName { get; set; }
        public string? ContactNo { get; set; }
        public string? Location { get; set; }
        public IFormFile? document { get; set; }
    }
}
