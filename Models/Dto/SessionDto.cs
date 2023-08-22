namespace MovieTicketApi.Models.Dto;

public class SessionDto
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public string Room { get; set; }
    public int MovieId { get; set; }
}