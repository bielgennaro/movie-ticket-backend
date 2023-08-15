using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketApi.Models;

public class Movie
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Genre { get; set; }
    
    [StringLength(1200, ErrorMessage = "A sinopse deve conter no máximo 1200 caracteres")]
    public string Synopsis { get; set; }
    public string Director { get; set; }
    public string BannerUrl { get; set; }
    public ICollection<Session> Sessions { get; set; }

    public Movie()
    {
    }

    public Movie(string genre,int movieId, string name, string synopsis, string director, string bannerUrl, ICollection<Session> sessions)
    {
        Genre = genre;
        Id = movieId;
        Name = name;
        Synopsis = synopsis;
        Director = director;
        BannerUrl = bannerUrl;
    }
}