using MovieTicketApi.Models.Interfaces;
using BCrypt.Net;

namespace MovieTicketApi.Services
{
    

    public class PasswordHashService : IPasswordHashService
    {
        public string HashPassword( string password )
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword( password, BCrypt.Net.BCrypt.GenerateSalt() );

            return hashedPassword;
        }

        public bool VerifyPassword( string inputPassword, string storedPasswordHash )
        {
            bool passwordMatches = BCrypt.Net.BCrypt.Verify( inputPassword, storedPasswordHash );

            return passwordMatches;
        }
    }

}
