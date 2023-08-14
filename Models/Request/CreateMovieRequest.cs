namespace MovieTicketApi.Models.Requests;

public class CreateMovieRequest
{
public string Title { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public string Trailer { get; set; }
    public string Director { get; set; }
    public string Cast { get; set; }
    public string Genre { get; set; }
    public string Duration { get; set; }
    public string ReleaseDate { get; set; }
    public string Language { get; set; }
    public string Subtitle { get; set; }
    public string Rated { get; set; }
    public string Format { get; set; }
    public string Status { get; set; }
    public string Price { get; set; }
}