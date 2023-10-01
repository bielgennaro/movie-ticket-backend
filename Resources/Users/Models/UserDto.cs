using Newtonsoft.Json;

namespace MovieTicketApi.Resources.Users.Models
{
    public sealed class UserDto
    {
        [JsonProperty( "id" )]
        public int Id { get; set; }

        [JsonProperty( "email" )]
        public string Email { get; set; }

        [JsonProperty( "isAdmin" )]
        public bool IsAdmin { get; set; }

        [JsonProperty( "hashedPassword" )]
        public string HashedPassword { get; set; }
    }
}