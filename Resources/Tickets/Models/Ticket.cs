#region


#endregion


using MovieTicketApi.Resources.Sessions.Models;
using MovieTicketApi.Resources.Users.Models;

namespace MovieTicketApi.Resources.Tickets.Models;

public sealed class Ticket
{
    public Ticket( int userId, int sessionId )
    {
        this.UserId = userId;
        this.SessionId = sessionId;
    }

    public int Id { get; private set; }

    public int SessionId { get; private set; }

    public Session Session { get; private set; } = null!;

    public int UserId { get; private set; }

    public User User { get; private set; } = null!;
}
