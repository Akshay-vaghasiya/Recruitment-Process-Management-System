namespace Backend.Dtos
{
    public class UserDto
    {
        public int PkUserId { get; set; }

        public string? FullName { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public DateOnly? JoiningDate { get; set; }

        public DateOnly? LeavingDate { get; set; }
    }
}
