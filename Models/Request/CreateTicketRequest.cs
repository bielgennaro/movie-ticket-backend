namespace MovieTicketApi.Models.Request;

public class CreateTicketRequest
{
    public int UserId { get; set; }

    public int MovieId { get; set; }

    public int SessionId { get; set; }
}