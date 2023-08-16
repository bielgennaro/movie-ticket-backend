#region

using MovieTicketApi.Models.Enums;

#endregion

namespace MovieTicketApi.Models;

public class Ticket
{
    public Ticket()
    {
    }

    public Ticket(double price, int ticketId, User userId, Session sessionId, string chairCoord, TicketStatus status)
    {
        TicketId = ticketId;
        Price = price;
        Id = userId;
        SessionId = sessionId;
        ChairCoord = chairCoord;
        Status = status;
    }

    public int TicketId { get; set; }
    public User Id { get; set; }
    private Session SessionId { get; set; }
    public string ChairCoord { get; set; }
    public double Price { get; set; }
    public TicketStatus Status { get; set; }
}