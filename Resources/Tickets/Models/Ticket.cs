#region


#endregion


using MovieTicketApi.Resources.Sessions.Models;
using MovieTicketApi.Resources.Users.Models;

namespace MovieTicketApi.Resources.Tickets.Models;

public class Ticket
{
    public Ticket()
    {
    }

    public Ticket( int userId, int sessionId )
    {
        this.UserId = userId;
        this.SessionId = sessionId;
    }

    public int Id { get; set; }

    public int SessionId { get; set; }

    public Session Session { get; set; } = null!;

    public int UserId { get; set; }

    public User User { get; set; } = null!;
}
