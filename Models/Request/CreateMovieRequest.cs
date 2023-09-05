namespace MovieTicketApi.Models.Requests;

public class CreateMovieRequest
{
    public string Title { get; set; }
    public string Gender { get; set; }
    public string Director { get; set; }
    public string Synopsis { get; set; }
    public string BannerUrl { get; set; }
}