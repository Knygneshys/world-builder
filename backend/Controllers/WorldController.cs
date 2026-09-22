using System.Security.Claims;
using backend.Auth;
using backend.Data;
using backend.Data.DTOs.Character;
using backend.Data.DTOs.World;
using backend.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Authorize]
[ApiController]
[Route("api/worlds")]
public class WorldController(WorldBuilderContext context) : ControllerBase
{
    private const int PageSize = 10;

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorldResponseDto>>> List([FromQuery] int page = 1)
    {
        if (page < 1) return BadRequest();

        return Ok(await context.Worlds.AsNoTracking()
            .OrderBy(world => world.Name)
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .Select(world => new WorldResponseDto(world.Id, world.Name, world.Description, world.Creator.UserName!))
            .ToListAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<WorldResponseDto>> Get(Guid id)
    {
        var world = await context.Worlds
          .Where(world => world.Id == id)
          .Select(world => new WorldResponseDto(world.Id, world.Name, world.Description, world.Creator.UserName!))
          .FirstOrDefaultAsync();

        return world is null ? NotFound() : Ok(world);
    }

    [HttpGet("{worldId:guid}/settlements/{settlementId:guid}/characters")]
    public async Task<ActionResult<IEnumerable<CharacterResponseDto>>> ListSettlementCharacters(
        Guid worldId,
        Guid settlementId)
    {
        var characters = await context.Settlements.AsNoTracking()
            .Where(settlement => settlement.Id == settlementId && settlement.WorldId == worldId)
            .Select(settlement => settlement.Characters
                .OrderBy(character => character.Name)
                .Select(character => new CharacterResponseDto(
                    character.Id,
                    character.Name,
                    character.Alias,
                    character.Species,
                    character.Age,
                    character.Gender,
                    character.Alignment,
                    character.Description,
                    settlement.Name))
                .ToList())
            .FirstOrDefaultAsync();

        return characters is null ? NotFound() : Ok(characters);
    }

    [HttpPost]
    public async Task<ActionResult<WorldResponseDto>> Create(WorldDto request)
    {
        if (IsInvalid(request)) return UnprocessableEntity();

        var creatorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(creatorId)) return Unauthorized();
        var creator = await context.Users.FindAsync(creatorId);
        if (creator is null) return Unauthorized();

        var world = new World
        {
            Id = Guid.NewGuid(),
            CreatorId = creatorId,
            Creator = creator,
            Name = request.Name.Trim(),
            Description = request.Description.Trim()
        };

        context.Worlds.Add(world);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { world.Id }, ToResponse(world));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(WorldDto request, Guid id)
    {
        if (IsInvalid(request)) return UnprocessableEntity();

        var world = await context.Worlds.Include(world => world.Creator)
            .FirstOrDefaultAsync(world => world.Id == id);
        if (world is null) return NotFound();
        if (!AuthUtils.IsAdmin(User) && world.CreatorId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            return Forbid();

        world.Name = request.Name.Trim();
        world.Description = request.Description.Trim();

        await context.SaveChangesAsync();
        return Ok(ToResponse(world));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var world = await context.Worlds.FindAsync(id);
        if (world is null) return NotFound();
        if (!AuthUtils.IsAdmin(User) && world.CreatorId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            return Forbid();
        
        if (await context.Settlements.AnyAsync(s => s.WorldId == id))
            return Conflict("Cannot delete a world that has settlements.");
        
        context.Worlds.Remove(world);
        await context.SaveChangesAsync();
        return NoContent();
    }

    private static bool IsInvalid(WorldDto request) =>
        string.IsNullOrWhiteSpace(request.Name) ||
        string.IsNullOrWhiteSpace(request.Description);

    private static WorldResponseDto ToResponse(World world) =>
        new(world.Id, world.Name, world.Description, world.Creator.UserName!);
}
