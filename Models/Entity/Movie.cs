namespace MovieTicketApi.Models;

public class Movie
{
    public Movie(string name, string genre, string director)
    {
        Name = name;
        Genre = genre;
        Director = director;
    }

    public Movie()
    {
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Genre { get; set; }
    private string Synopsis { get; set; }
    public string Director { get; set; }
    private string BannerUrl { get; set; }
    
    public int SessionId { get; set; }
    public ICollection<Session> SessionsList { get; set; }
}