namespace MovieTicketApi.Models.Requests;

public class CreateTicketRequest
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = new User();
    public int SessionId { get; set; }
    public Session Session { get; set; } = new Session();
}