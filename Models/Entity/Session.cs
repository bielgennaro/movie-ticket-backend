﻿namespace MovieTicketApi.Models;

public class Session
{
    public Session()
    {
    }

    public Session(DateTime dateTime, string room)
    {
        DateTime = dateTime;
        Room = room;
    }

    public int Id { get; set; }
    public string Room { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;
    public int MovieId { get; set; }
    public Movie Movie { get; set; }
}