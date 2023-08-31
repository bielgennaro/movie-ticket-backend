namespace MovieTicketApi.Models.Requests;

public class CreateUserRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool isAdminn { get; set; }
}