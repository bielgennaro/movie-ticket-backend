namespace MovieTicketApi.Models.Requests;

public class CreateSessionRequest
{
    public string Room { get; set; }

    public DateTime DateTime { get; set; }

    public int MovieId { get; set; }
}