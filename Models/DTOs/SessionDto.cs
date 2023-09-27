using Newtonsoft.Json;

namespace MovieTicketApi.Models.DTOs;

public sealed class SessionDto
{
    [JsonProperty( "room" )]
    public string Room { get; set; }

    [JsonProperty( "dateTime" )]
    public DateTime DateTime { get; set; }

    [JsonProperty( "movie" )]
    public MovieDto Movie { get; set; }
}
