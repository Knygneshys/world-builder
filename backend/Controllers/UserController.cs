using backend.Auth;
using backend.Data;
using backend.Data.DTOs.Auth;
using backend.Data.DTOs.User;
using backend.Data.Entities;
using backend.Data.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(
    UserManager<IdentityUser> userManager,
    WorldBuilderContext dbContext,
    JwtTokenProvider jwtTokenProvider) : ControllerBase
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
        
        var accessToken = await jwtTokenProvider.Provide(user);
        
        var refreshToken = new RefreshToken()
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = JwtTokenProvider.GenerateRefreshToken(),
            ExpiresOnUtc = DateTime.UtcNow.AddDays(7)
        };
        
        await dbContext.RefreshTokens.AddAsync(refreshToken);
        await dbContext.SaveChangesAsync();
        
        return Ok(new LoginResponse(accessToken, refreshToken.Token));
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken(RefreshRequest request)
    {
        var refreshToken = await dbContext.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken);
        
        if(refreshToken is null || refreshToken.ExpiresOnUtc < DateTime.UtcNow)
        {
            return Unauthorized("Invalid or expired refresh token.");
        }
        
        var accessToken = await jwtTokenProvider.Provide(refreshToken.User);
        
        refreshToken.Token = JwtTokenProvider.GenerateRefreshToken();
        refreshToken.ExpiresOnUtc = DateTime.UtcNow.AddDays(7);
        
        await dbContext.SaveChangesAsync();

        return Ok(new LoginResponse(accessToken, refreshToken.Token));
    }
    
    [HttpDelete("{userId}/logout")]
    public async Task<IActionResult> Logout(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        
        if(user == null)
        {
            return NotFound("User not found.");
        }
        
        await jwtTokenProvider.RevokeRefreshToken(userId);

        return Ok();
    }
}
