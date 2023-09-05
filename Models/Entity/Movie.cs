namespace MovieTicketApi.Models;

public class Movie
{
    public Movie()
    {
    }

    public Movie(string title, string gender, string synopsis, string director, string bannerUrl)
    {
        Title = title;
        Gender = gender;
        Synopsis = synopsis;
        Director = director;
        BannerUrl = bannerUrl;
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public string Gender { get; set; }
    public string Synopsis { get; set; }
    public string Director { get; set; }
    public string BannerUrl { get; set; }
}