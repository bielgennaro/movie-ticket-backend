namespace MovieTicketApi.Models.Requests;

public class CreateMovieRequest
{
    public string Name { get; set; }
    public string Genre { get; set; }
    public string Director { get; set; }
    public string Synopsis { get; set; }
    public string BannerUrl { get; set; }
}