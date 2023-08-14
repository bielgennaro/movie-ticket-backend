using System.ComponentModel.DataAnnotations.Schema;
using MovieTicketApi.Models.Enums;

namespace MovieTicketApi.Models;

public class Ticket
{
    public int TicketId { get; set; }
    [ForeignKey("UserId")] public User UserId { get; set; }
    [ForeignKey("SessionId")] private Session Session { get; set; }
    public string ChairCoord { get; set; }
    public double Price { get; set; }
    public TicketStatus Status { get; set; }

    public Ticket()
    {
    }

    public Ticket(double price, int ticketId, User userId, Session sessionId, string chairCoord, TicketStatus status)
    {
        TicketId = ticketId;
        Price = price;
        UserId = userId;
        Session = sessionId;
        ChairCoord = chairCoord;
        Status = status;
    }
}