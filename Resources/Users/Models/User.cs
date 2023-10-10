#region


#endregion


using System.ComponentModel.DataAnnotations.Schema;

using MovieTicketApi.Validation;

namespace MovieTicketApi.Resources.Users.Models;

public sealed class User
{
    public User()
    {
    }

    public User( string email, bool isAdmin, string password, string hashedPassword )
    {
        this.ValidateDomain( email, password );
        this.IsAdmin = isAdmin;
        this.HashedPassword = hashedPassword;
    }

    public int Id { get; private set; }

    public string Email { get; set; }

    [NotMapped]
    public string Password { get; set; }

    public bool IsAdmin { get; set; }

    [NotMapped]
    public string HashedPassword { get; private set; }


    private void ValidateDomain( string email, string password )
    {
        EntityValidationException.When( string.IsNullOrEmpty( email ), "Invalid email. Password is required" );

        EntityValidationException.When( email.Length < 3, "Invalid email, too short, minimum 3 characters" );

        EntityValidationException.When( string.IsNullOrEmpty( password ), "Invalid password. Password is required" );

        this.Email = email;
        this.Password = password;
    }
}