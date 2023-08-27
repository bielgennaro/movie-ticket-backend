namespace MovieTicketApi.Models.Dto;

public class TicketDto
{
    public int Id { get; set; }
    public SessionDto Session { get; set; }
    public int UserId { get; set; }
}