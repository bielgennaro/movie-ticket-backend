namespace MovieTicketApi.Services
{
    public class PasswordHashService
    {
        public string HashPassword( string password )
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword( password );
            return hashedPassword;
        }

        public bool VerifyPassword( string password, string hashedPassword )
        {
            return BCrypt.Net.BCrypt.Verify( password, hashedPassword );
        }
    }
}