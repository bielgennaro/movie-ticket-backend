#region

using MovieTicketApi.Models.Enums;

#endregion

namespace MovieTicketApi.Models;

public class Ticket
{
    public Ticket()
    {
    }

    public Ticket(double price, string chairCoord, TicketStatus status)
    {
        Price = price;
        ChairCoord = chairCoord;
        Status = status;
    }

    public int TicketId { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }
    private Session SessionId { get; set; }
    public string ChairCoord { get; set; }
    public double Price { get; set; }
    public TicketStatus Status { get; set; }
}