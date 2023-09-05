using System.ComponentModel.DataAnnotations;

namespace MovieTicketApi.Models.Dto;

public class MovieDto
{
    public string Genre { get; set; }
    public string Synopsis { get; set; }
    public string Director { get; set; }
    public string BannerUrl { get; set; }
}