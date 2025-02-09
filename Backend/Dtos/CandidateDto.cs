namespace Backend.Dtos
{
    public class CandidateDto
    {
        public string? FullName { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public IFormFile? Resume { get; set; }

        public int? YearsOfExperience { get; set; }
    }
}
