namespace MovieTicketApi.Models.Request;

public class CreateUserRequest
{
    public string Email { get; set; }

    public string Password { get; set; }

    public bool IsAdmin { get; set; }
}