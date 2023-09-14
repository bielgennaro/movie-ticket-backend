#region


#endregion


namespace MovieTicketApi.Models.Entity;

public class Ticket
{
    public Ticket()
    {
    }

    public Ticket( int userId, int movieId, int sessionId )
    {
        this.UserId = userId;
        this.MovieId = movieId;
        this.SessionId = sessionId;
    }

    public int Id { get; set; }

    public int SessionId { get; set; }

    public Session Session { get; set; }

    public int UserId { get; set; }

    public int MovieId { get; set; }

    public Movie Movie { get; set; }

    public User User { get; set; }
}
