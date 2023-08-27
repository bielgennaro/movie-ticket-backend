namespace MovieTicketApi.Models;

public class Movie
{
    public Movie()
    {
    }

    public Movie(string gender, string synopsis, string director)
    {
        Gender = gender;
        Synopsis = synopsis;
        Director = director;
    }

    public int Id { get; set; }
    public string Gender { get; set; }
    public string Synopsis { get; set; }
    public string Director { get; set; }
    public string BannerUrl { get; set; }
}