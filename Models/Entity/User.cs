#region


#endregion


using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketApi.Models.Entity;

public class User
{
    public User()
    {
    }

    public User( string email, bool isAdmin, string password, string hashedPassword )
    {
        this.Email = email;
        this.IsAdmin = isAdmin;
        this.Password = password;
        this.HashedPassword = hashedPassword;
    }

    public int Id { get; set; }

    public string Email { get; set; }

    [NotMapped]
    public string Password { get; set; }

    public bool IsAdmin { get; set; }

    [NotMapped]
    public string HashedPassword { get; }
}