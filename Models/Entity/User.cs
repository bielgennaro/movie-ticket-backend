#region


#endregion


namespace MovieTicketApi.Models.Entity;

public class User
{
    public User()
    {
    }

    public User( string email, bool isAdmin, string password )
    {
        this.Email = email;
        this.IsAdmin = isAdmin;
        this.Password = password;
    }

    public int Id { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public bool IsAdmin { get; set; }

    public string PasswordHash { get; internal set; }
}