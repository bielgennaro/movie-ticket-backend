using MovieTicketApi.Models.Enums;

namespace MovieTicketApi.Models.Dto;

public class TicketDto
{
    public int TicketId { get; set; }
    public int UserId { get; set; }
    public int SessionId { get; set; }
    public string ChairCoord { get; set; }
    public double Price { get; set; }
    public TicketStatus Status { get; set; }
}