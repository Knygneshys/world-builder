using backend.Data;
using backend.Data.DTOs;
using backend.Data.Entities;
using backend.Data.Entities.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController]
[Route("api/settlements")]
public class SettlementController(WorldBuilderContext context) : ControllerBase
{
    private const int PageSize = 10;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Settlement>>> List(
        [FromQuery] SettlementType? type,
        [FromQuery] int? population,
        [FromQuery] int page = 1)
    {
        if (page < 1 ||
            population < 0 ||
            type.HasValue && !Enum.IsDefined(type.Value))
        {
            return BadRequest();
        }

        var query = context.Settlements.AsNoTracking();

        if (type.HasValue) query = query.Where(settlement => settlement.Type == type);
        if (population.HasValue) query = query.Where(settlement => settlement.Population == population);

        return Ok(await query
            .OrderBy(settlement => settlement.Name)
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Settlement>> Get(Guid id)
    {
        var settlement = await context.Settlements.AsNoTracking()
            .FirstOrDefaultAsync(settlement => settlement.Id == id);

        return settlement is null ? NotFound() : Ok(settlement);
    }

    [HttpPost]
    public async Task<ActionResult<Settlement>> Create(SettlementDto request)
    {
        var isWorldInDatabase = await context.Worlds.AnyAsync(world => world.Id == request.WorldId);
        if (IsInvalid(request) || !isWorldInDatabase)
            return UnprocessableEntity();

        var settlement = new Settlement
        {
            Id = Guid.NewGuid(),
            Name = request.Name.Trim(),
            Type = request.Type,
            Description = request.Description.Trim(),
            Population = request.Population,
            WorldId = request.WorldId,
            World = null!
        };

        context.Settlements.Add(settlement);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { settlement.Id }, settlement);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(SettlementDto request, Guid id)
    {
        if (IsInvalid(request) || !await context.Worlds.AnyAsync(world => world.Id == request.WorldId))
            return UnprocessableEntity();

        var settlement = await context.Settlements.FindAsync(id);
        if (settlement is null) return NotFound();

        settlement.Name = request.Name.Trim();
        settlement.Type = request.Type;
        settlement.Description = request.Description.Trim();
        settlement.Population = request.Population;
        settlement.WorldId = request.WorldId;

        await context.SaveChangesAsync();
        return Ok(settlement);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var settlement = await context.Settlements.FindAsync(id);
        if (settlement is null) return NotFound();

        context.Settlements.Remove(settlement);
        await context.SaveChangesAsync();
        return NoContent();
    }

    private static bool IsInvalid(SettlementDto request) =>
        string.IsNullOrWhiteSpace(request.Name) ||
        string.IsNullOrWhiteSpace(request.Description) ||
        request.Population < 0 ||
        !Enum.IsDefined(request.Type);
}
