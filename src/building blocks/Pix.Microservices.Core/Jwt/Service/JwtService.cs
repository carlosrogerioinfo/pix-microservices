using Microsoft.IdentityModel.Tokens;
using Pix.Microservices.Core.Jwt.Settings;
using Pix.Microservices.Domain.Http.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pix.Microservices.Core.Jwt
{
    public class JwtService : IJwtService
    {
        public string GenerateToken(UserResponse user, JwtSettings jwtSettings)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
            };

            claims.Add(new Claim(ClaimTypes.Role, user.UserType));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = jwtSettings.Issuer,
                Audience = jwtSettings.Audience,
                Expires = DateTime.UtcNow.AddMinutes(jwtSettings.TimeValidationToken),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
