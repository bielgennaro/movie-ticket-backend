#region

using System.ComponentModel.DataAnnotations;

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

    [EmailAddress(ErrorMessage = "Insira um email válido!")]
    public string Email { get; set; }

    [StringLength(10, ErrorMessage = "Sua senha deve conter entre 8 e 10 caracteres", MinimumLength = 8)]
    public string Password { get; set; }

    public bool IsAdmin { get; set; }

    public ICollection<Ticket> Tickets { get; set; }
}