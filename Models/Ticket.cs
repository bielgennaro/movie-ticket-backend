using System.ComponentModel.DataAnnotations.Schema;
using MovieTicketApi.Models.Enums;

namespace MovieTicketApi.Models;

public class Ticket
{
    public int TicketId { get; set; }

    [ForeignKey("UserId")] private User User { get; set; }

    [ForeignKey("SessionId")] private Session Session { get; set; }
    public string ChairCoord { get; set; }
    public TicketStatus Status { get; set; }

    public Ticket()
    {
    }

    public Ticket(int ticketId, User userId, Session sessionId, string chairCoord, TicketStatus status)
    {
        TicketId = ticketId;
        User = userId;
        Session = sessionId;
        ChairCoord = chairCoord;
        Status = status;
    }
}