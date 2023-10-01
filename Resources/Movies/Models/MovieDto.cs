using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

namespace MovieTicketApi.Resources.Movies.Models;

public sealed class MovieDto
{
    [JsonProperty( "genre" )]
    public string Genre { get; set; }

    [JsonProperty( "synopsis" )]
    public string Synopsis { get; set; }

    [JsonProperty( "director" )]
    public string Director { get; set; }

    [JsonProperty( "bannerUrl" )]
    [Url]
    public string BannerUrl { get; set; }
}