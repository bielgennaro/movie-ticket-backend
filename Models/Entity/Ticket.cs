#region


#endregion

namespace MovieTicketApi.Models;

public class Ticket
{
    public Ticket()
    {
    }

    public Ticket( int id, int sessionId, int userId, Session session, User user )
    {
        this.Id = id;
        this.SessionId = sessionId;
        this.UserId = userId;
        this.Session = session;
        this.User = user;
    }

    public int Id { get; set; }
    public int SessionId { get; set; }
    public Session Session { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}
