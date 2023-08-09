using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MovieTicketApi.Models;

public class Session
{
    public int SessionId { get; set; }
    public DateTime DateTime { get; set; }
    public int Room { get; set; }
    
    public ICollection<MovieSession> MovieSessions { get; set; } = new List<MovieSession>();
    
    public Session()
    {
    }

    public Session(int sessionId, DateTime dateTime, int room)
    {
        SessionId = sessionId;
        DateTime = dateTime;
        Room = room;
    }
}