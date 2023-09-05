namespace MovieTicketApi.Models.Dto
{
    public class UserDto
    {
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
    }
}
