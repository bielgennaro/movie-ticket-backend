namespace MovieTicketApi.Models.Dto;

public class SessionDto
{
    public string Room { get; set; }
    public DateTime DateTime { get; set; }
    public Movie Movie { get; set; }
    
    public SessionDto() { }

    public SessionDto(Movie movie)
    {
        Movie = movie;
    }
}
