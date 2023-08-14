using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MovieTicketApi.Models;

public class Session
{
    public int SessionId { get; set; }
    public DateTime DateTime { get; set; }
    public int Room { get; set; }
    
    [ForeignKey("MovieId")]
    public int MovieId { get; set; }
    public virtual Movie Movie { get; set; }

    public Session()
    {
    }

    public Session(int sessionId, DateTime dateTime, int room, int movieId)
    {
        SessionId = sessionId;
        DateTime = dateTime;
        Room = room;
        MovieId = movieId;
    }
    
}