namespace MovieTicketApi.Models.Dto;

public class SessionDto
{
    public int Id { get; set; }
    public string Room { get; set; }
    public DateTime DateTime { get; set; }
    public MovieDto Movie { get; set; }
}
