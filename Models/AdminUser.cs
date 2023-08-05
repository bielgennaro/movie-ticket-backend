namespace MovieTicketApi.Models;

public class AdminUser
{
    public User UserId { get; set; }
    public User Email { get; set; }

    public AdminUser()
    {
    }

    public AdminUser(User userId, User email)
    {
        UserId = userId;
        Email = email;
    }
}