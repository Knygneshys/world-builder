using System.Security.Claims;
using System.Text;
using backend.Data;
using backend.Data.DTOs.User;
using backend.Data.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace backend.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(
    UserManager<IdentityUser> userManager,
    WorldBuilderContext dbContext,
    IConfiguration configuration) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequestDto request)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        
        var user = new IdentityUser { UserName = request.Username, Email = request.Email };
        var creationResult = await userManager.CreateAsync(user, request.Password);
        if(!creationResult.Succeeded)
        {
            return BadRequest(creationResult.Errors);
        }
        
        var roleResult = await userManager.AddToRoleAsync(user, nameof(Roles.Player));
        if(!roleResult.Succeeded)
        {
            return BadRequest(roleResult.Errors);
        }
        
        await transaction.CommitAsync();
        
        return Ok();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if(user == null || !await userManager.CheckPasswordAsync(user, request.Password))
        {
            return Unauthorized();
        }
        
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
        
        var accessToken = tokenHandler.CreateToken(tokenDescriptor);
        
        return Ok(new { accessToken });
    }
}
