namespace MovieTicketApi.Models;

public class Movie
{
    public int MovieId { get; set; }
    public string Name { get; set; }
    public string Synopsis { get; set; }
    public string Director { get; set; }
    public string BannerUrl { get; set; }
    
    public Movie()
    {
    }
    
    public Movie(int movieId, string name, string synopsis, string director, string bannerUrl)
    {
        MovieId = movieId;
        Name = name;
        Synopsis = synopsis;
        Director = director;
        BannerUrl = bannerUrl;
    }
}