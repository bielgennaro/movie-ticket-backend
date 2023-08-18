#region

using System.ComponentModel.DataAnnotations;

#endregion

namespace MovieTicketApi.Models;

public class User
{
    public User()
    {
        TicketsList = new List<Ticket>();
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

    [StringLength(12, ErrorMessage = "Sua senha deve conter entre 6 e 12 caracteres", MinimumLength = 6)]
    public string Password { get; set; }

    public bool IsAdmin { get; set; }

    public ICollection<Ticket> TicketsList { get; set; }
}