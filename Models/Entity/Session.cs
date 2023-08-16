namespace MovieTicketApi.Models;

public class Session
{
    public Session()
    {
    }

    public Session(int sessionId, DateTime dateTime, int room, int movieId)
    {
        SessionId = sessionId;
        DateTime = dateTime;
        Room = room;
        MovieId = movieId;
    }

    public int SessionId { get; set; }
    public DateTime DateTime { get; set; }
    public int Room { get; set; }
    public int MovieId { get; set; }
    public Movie SessionMovies { get; set; }
}