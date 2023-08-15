namespace MovieTicketApi.Models.Requests;

public class CreateSessionRequest
{
    public int Room { get; set; }
    public DateTime DateTime { get; set; }
}