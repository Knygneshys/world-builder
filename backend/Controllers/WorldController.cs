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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorldResponseDto>>> List([FromQuery] int page = 1)
    {
        if (page < 1) return BadRequest();

        return Ok(await context.Worlds.AsNoTracking()
            .OrderBy(world => world.Name)
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .Select(world => new WorldResponseDto(world.Id, world.Name, world.Description))
            .ToListAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<WorldResponseDto>> Get(Guid id)
    {
        var world = await context.Worlds
          .Where(world => world.Id == id)
          .Select(world => new WorldResponseDto(world.Id, world.Name, world.Description))
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

        var world = new World
        {
            Id = Guid.NewGuid(),
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

        var world = await context.Worlds.FindAsync(id);
        if (world is null) return NotFound();

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
        new(world.Id, world.Name, world.Description);
}
