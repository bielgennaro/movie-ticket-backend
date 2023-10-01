using MovieTicketApi.Resources.Sessions.Models;

using Newtonsoft.Json;

namespace MovieTicketApi.Resources.Tickets.Models;

public sealed class TicketDto
{
    [JsonProperty( "session" )]
    public SessionDto Session { get; set; }

    [JsonProperty( "userId" )]
    public int UserId { get; set; }
}