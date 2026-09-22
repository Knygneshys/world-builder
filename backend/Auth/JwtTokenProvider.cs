using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace backend.Auth;

public class JwtTokenProvider(
    UserManager<IdentityUser> userManager,
    IConfiguration configuration)
{
    public async Task<string> Provide(IdentityUser user)
    {
        var roles = await userManager.GetRolesAsync(user);
        
        var secretKey = configuration["Jwt:SecretKey"] ?? throw new InvalidOperationException("JWT secret key is not configured.");
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claims =
        [
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            .. roles.Select(r => new Claim(ClaimTypes.Role, r))
        ];

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:ExpiresInMinutes")), 
            SigningCredentials = credentials,
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"]
        };

        var tokenHandler = new JsonWebTokenHandler();
        
        return tokenHandler.CreateToken(tokenDescriptor);
    }
}
