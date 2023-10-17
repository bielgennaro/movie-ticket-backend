using Newtonsoft.Json;

namespace MovieTicketApi.Models.Dtos;

public sealed class TicketDto
{
    [JsonProperty( "session" )]
    public SessionDto Session { get; set; }

    [JsonProperty( "userId" )]
    public int UserId { get; set; }
}