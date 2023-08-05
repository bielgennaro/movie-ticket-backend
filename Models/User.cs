namespace MovieTicketApi.Models;

public class User
{
    public int UserId { get; set; }
    public string Email { get; set; }
    private string Password { get; set; }
    public bool IsAdmin { get; set; }

    public User()
    {
    }

    public User(int userId, string email, string password, bool isAdmin)
    {
        UserId = userId;
        Email = email;
        Password = password;
        IsAdmin = isAdmin;
    }
    
}