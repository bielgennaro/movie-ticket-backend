using System.ComponentModel.DataAnnotations;

namespace MovieTicketApi.Models;

public class Movie
{
    [Key]
    public int MovieId { get; set; }
    public string Name { get; set; }
    
    [StringLength(1200, ErrorMessage = "A sinopse deve conter no máximo 1200 caracteres")]
    public string Synopsis { get; set; }
    public string Director { get; set; }
    public string BannerUrl { get; set; }
    public ICollection<MovieSession> MovieSessions { get; set; } = new List<MovieSession>();

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