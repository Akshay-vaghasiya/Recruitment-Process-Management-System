namespace Backend.Dtos
{
    public class RegisterDto
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public DateOnly? JoiningDate { get; set; }
        public List<string>? Roles { get; set; }
    }
}

