#region


#endregion

namespace MovieTicketApi.Models;

public class Ticket
{
    public Ticket()
    {
    }

    public Ticket(int id, int sessionId, int userId, Session session, User user)
    {
        Id = id;
        SessionId = sessionId;
        UserId = userId;
        Session = session;
        User = user;
    }

    public int Id { get; set; }
    public int SessionId { get; set; }
    public Session Session { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }


}