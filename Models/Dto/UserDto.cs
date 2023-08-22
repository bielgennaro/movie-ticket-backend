using System.ComponentModel.DataAnnotations;

namespace MovieTicketApi.Models.Dto;

public class UserDto
{
    public int Id { get; set; }

    [EmailAddress(ErrorMessage = "Insira um email válido.")]
    public string Email { get; set; }

    [StringLength(12, ErrorMessage = "A senha deve conter entre 6 e 12 caracteres.", MinimumLength = 6)]
    public string Password { get; set; }

    public bool IsAdmin { get; set; }
}