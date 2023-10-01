using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.IdentityModel.Tokens;

using MovieTicketApi.Models.Entity;

namespace MovieTicketApi.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService( IConfiguration configuration )
        {
            this._configuration = configuration ?? throw new ArgumentNullException( nameof( configuration ) );
        }

        public string GenerateToken( User user )
        {
            byte[] key = Encoding.ASCII.GetBytes( this._configuration["Jwt:SecretKey"] );
            string? issuer = this._configuration["Jwt:Issuer"];
            string? audience = this._configuration["Jwt:Audience"];
            int expiryInMinutes = Convert.ToInt32( this._configuration["Jwt:ExpiryInMinutes"] );

            ClaimsIdentity claims = GenerateClaims( user );

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity( claims ),
                Expires = DateTime.UtcNow.AddMinutes( expiryInMinutes ),
                SigningCredentials = new SigningCredentials( new SymmetricSecurityKey( key ), SecurityAlgorithms.HmacSha256Signature ),
                Issuer = issuer,
                Audience = audience
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken( tokenDescriptor );

            return tokenHandler.WriteToken( token );
        }

        public ClaimsPrincipal? GetPrincipalFromToken( string token )
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes( this._configuration["Jwt:SecretKey"] );

            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey( key ),
                ValidateIssuer = true,
                ValidIssuer = this._configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = this._configuration["Jwt:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                ClaimsPrincipal principal = tokenHandler.ValidateToken( token, tokenValidationParameters, out SecurityToken? validatedToken );
                if( IsJwtWithValidSecurityAlgorithm( validatedToken ) )
                {
                    return principal;
                }
            }
            catch( Exception e )
            {
                throw new Exception( e.Message );
            }

            return null;

        }

        public bool VerifyToken( string token, int id )
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes( this._configuration["Jwt:SecretKey"] );

            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey( key ),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                tokenHandler.ValidateToken( token, tokenValidationParameters, out _ );
                return true;
            }
            catch( Exception )
            {
                return false;
            }
        }


        private static ClaimsIdentity GenerateClaims( User user )
        {
            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Password),
                new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "Client")
            };

            return new ClaimsIdentity( claims );
        }

        private static bool IsJwtWithValidSecurityAlgorithm( SecurityToken validatedToken )
        {
            return ( validatedToken is JwtSecurityToken jwtSecurityToken ) &&
                jwtSecurityToken.Header.Alg.Equals( SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase );
        }
    }
}
