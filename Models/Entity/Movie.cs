namespace MovieTicketApi.Models;

public class Movie
{
    public Movie()
    {
    }

    public Movie( string title, string gender, string synopsis, string director, string bannerUrl )
    {
        this.Title = title;
        this.Gender = gender;
        this.Synopsis = synopsis;
        this.Director = director;
        this.BannerUrl = bannerUrl;
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public string Gender { get; set; }
    public string Synopsis { get; set; }
    public string Director { get; set; }
    public string BannerUrl { get; set; }
}