namespace MovieTicketApi.Request;

public class CreateSessionRequest
{
    public string Room { get; set; }

    public DateTime DateTime { get; set; }

    public int MovieId { get; set; }
}