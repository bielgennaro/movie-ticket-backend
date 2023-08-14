using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MovieTicketApi.Models;

public class User
{
    public int UserId { get; set; }

    [EmailAddress(ErrorMessage = "Insira um email válido!")]
    public string Email { get; set; }

    [StringLength(10, ErrorMessage = "Sua senha deve conter entre 8 e 10 caracteres", MinimumLength = 8)]
    public string Password { get; set; }

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