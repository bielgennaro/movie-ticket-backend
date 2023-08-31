namespace MovieTicketApi.Models.Dto
{
    public class UserDto
    {
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }

        public UserDto() { }

        public UserDto(string email, bool isAdmin, string passwordHash, string passwordSalt)
        {
            Email = email;
            IsAdmin = isAdmin;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }
    }
}
