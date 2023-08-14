namespace MovieTicketApi.Models.Requests;

public class CreateTicketRequest
{
    public int SessionId { get; set; }
    public string ChairCoord { get; set; }
    public double Price { get; set; }
}