using Newtonsoft.Json;

namespace MovieTicketApi.Models.DTOs
{
    public sealed class UserDto
    {
        [JsonProperty( "id" )]
        public int Id { get; set; }

        [JsonProperty( "email" )]
        public string Email { get; set; }

        [JsonProperty( "isAdmin" )]
        public bool IsAdmin { get; set; }

        [JsonProperty( "passwordHash" )]
        public string PasswordHash { get; set; }
    }
}