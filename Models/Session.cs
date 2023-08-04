using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MovieTicketApi.Models;

public class Session
{
    public int SessionId { get; set; }
    public DateTime DateTime { get; set; }
    public int Room { get; set; }

    [ForeignKey("MovieId")] public Movie Movie { get; set; }


    public Session()
    {
    }

    public Session(int sessionId, DateTime dateTime, int room, Movie movie)
    {
        SessionId = sessionId;
        DateTime = dateTime;
        Room = room;
        Movie = movie;
    }
}