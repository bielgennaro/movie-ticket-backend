namespace MovieTicketApi.Interfaces
{
    public interface IPasswordHashService
    {
        string HashPassword( string password );

        bool VerifyPassword( string inputPassword, string storedPasswordHash );
    }
}