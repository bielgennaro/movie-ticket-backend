using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MovieTicketApi.Models;

public class Session
{
    [Key] public int SessionId { get; set; }
    public DateTime DateTime { get; set; }
    public int Room { get; set; }
    public Movie Movie { get; set; }
    public string MovieHour { get; set; }

    public Session()
    {
    }

    public Session(int sessionId, DateTime dateTime, int room, Movie movie, string movieHour)
    {
        SessionId = sessionId;
        DateTime = dateTime;
        Room = room;
        Movie = movie;
        MovieHour = movieHour;
    }
}