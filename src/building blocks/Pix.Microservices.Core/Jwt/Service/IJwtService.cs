using Pix.Microservices.Core.Jwt.Settings;
using Pix.Microservices.Domain.Http.Response;

namespace Pix.Microservices.Core.Jwt
{
    public interface IJwtService
    {
        string GenerateToken(UserResponse user, JwtSettings jwtSettings);
    }
}
