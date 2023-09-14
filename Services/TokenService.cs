using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.IdentityModel.Tokens;

using MovieTicketApi.Models.Entity;

namespace MovieTicketApi.Services
{
    public class TokenService
    {
        public TokenService()
        {
        }

        public string Generate( User user )
        {
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes( "1F2A3B4C5D6E7F8A9B0C1D2E3F4A5B6C7D8E9F0A1B2C3D4E5F6" );

            var credentials = new SigningCredentials( new SymmetricSecurityKey( key ), SecurityAlgorithms.HmacSha256Signature );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims( user ),
                SigningCredentials = credentials,
                Expires = System.DateTime.UtcNow.AddHours( 2 ),
            };

            var token = handler.CreateToken( tokenDescriptor );

            return handler.WriteToken( token );
        }

        private static ClaimsIdentity GenerateClaims( User user )
        {
            var ci = new ClaimsIdentity();

            ci.AddClaim( new Claim( ClaimTypes.Email, user.Email ) );
            ci.AddClaim( new Claim( ClaimTypes.Name, user.Password ) );
            ci.AddClaim( new Claim( ClaimTypes.Role, user.IsAdmin ? "Admin" : "Client" ) );

            return ci;
        }
    }
}
