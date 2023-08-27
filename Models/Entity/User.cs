#region


#endregion

namespace MovieTicketApi.Models;

public class User
{
    public User()
    {
    }

    public User(int id, string email, string password, bool isAdmin)
    {
        Id = id;
        Email = email;
        Password = password;
        IsAdmin = isAdmin;
    }

    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; }
}