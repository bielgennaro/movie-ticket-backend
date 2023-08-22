using System.ComponentModel.DataAnnotations;

namespace MovieTicketApi.Models;

public class Movie
{
    public Movie()
    {
    }

    public Movie(string name, string genre, string director, string synopsis, string bannerUrl)
    {
        Name = name;
        Genre = genre;
        Director = director;
        Synopsis = synopsis;
        BannerUrl = bannerUrl;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Genre { get; set; }
    public string Synopsis { get; set; }
    public string Director { get; set; }
    public string BannerUrl { get; set; }
    public ICollection<Session> SessionsList { get; set; }
}