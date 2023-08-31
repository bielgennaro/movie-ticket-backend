#region


#endregion

namespace MovieTicketApi.Models;

public class User
{
    public User()
    {
    }

    public User(string email, bool isAdmin, string password)
    {
        Email = email;
        IsAdmin = isAdmin;
        Password = password;
    }

    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; }
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
}