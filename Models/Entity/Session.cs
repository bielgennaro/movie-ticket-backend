namespace MovieTicketApi.Models.Entity;

public class Session
{
    public Session()
    {
    }

    public Session( DateTime dateTime, string room, int movieId )
    {
        this.DateTime = dateTime;
        this.Room = room;
        this.MovieId = movieId;
    }

    public int Id { get; set; }

    public string Room { get; set; }

    public DateTime DateTime { get; set; } = DateTime.Now;

    public int MovieId { get; set; }

    public Movie Movie { get; set; }
}
