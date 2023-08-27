#region


#endregion

namespace MovieTicketApi.Models;

public class Ticket
{
    public Ticket()
    {
    }

    public Ticket(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
    public virtual int  SessionId { get; set; }
    public Session Session { get; set; }
    public virtual int UserId { get; set; }
    public User User { get; set; }


}