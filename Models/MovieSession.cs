using System.ComponentModel.DataAnnotations;

namespace MovieTicketApi.Models;

public class MovieSession
{
    [Key]
    public int MovieSessionId { get; set; }
    public int MovieId { get; set; }
    public Movie Movie { get; set; }
    public int SessionId { get; set; }
    public Session Session { get; set; }
}