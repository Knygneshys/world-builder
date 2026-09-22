using backend.Auth;
using backend.Data;
using backend.Data.DTOs.User;
using backend.Data.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        
        return Ok(new { accessToken });
    }
}
