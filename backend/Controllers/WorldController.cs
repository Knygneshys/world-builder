using backend.Data;
using backend.Data.DTOs.World;
using backend.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

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
        var world = await context.Worlds.AsNoTracking()
            .Select(world => new WorldResponseDto(world.Id, world.Name, world.Description))
            .FirstOrDefaultAsync(world => world.Id == id);

        return world is null ? NotFound() : Ok(world);
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
