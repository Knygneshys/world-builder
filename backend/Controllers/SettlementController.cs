using backend.Data;
using backend.Data.DTOs.Settlement;
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
    public async Task<ActionResult<IEnumerable<SettlementResponseDto>>> List(
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
            .Select(settlement => new SettlementResponseDto(
                settlement.Id,
                settlement.Name,
                settlement.Type,
                settlement.Description,
                settlement.Population,
                settlement.World.Name))
            .ToListAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SettlementResponseDto>> Get(Guid id)
    {
        var settlement = await context.Settlements.AsNoTracking()
            .Select(settlement => new SettlementResponseDto(
                settlement.Id,
                settlement.Name,
                settlement.Type,
                settlement.Description,
                settlement.Population,
                settlement.World.Name))
            .FirstOrDefaultAsync(settlement => settlement.Id == id);

        return settlement is null ? NotFound() : Ok(settlement);
    }

    [HttpGet("{id:guid}/predominant-species")]
    public async Task<ActionResult<PredominantSpeciesResponseDto>> GetPredominantSpecies(Guid id)
    {
        var cityName = await context.Settlements.AsNoTracking()
            .Where(settlement => settlement.Id == id)
            .Select(settlement => settlement.Name)
            .FirstOrDefaultAsync();
        if (cityName is null) return NotFound();

        var speciesCounts = await context.Characters.AsNoTracking()
            .Where(character => character.SettlementId == id)
            .GroupBy(character => character.Species)
            .Select(group => new { Species = group.Key, Count = group.Count() })
            .ToListAsync();
        if (speciesCounts.Count == 0) return NotFound("No characters found!");

        var maximum = speciesCounts.Max(item => item.Count);
        var species = speciesCounts
            .Where(item => item.Count == maximum)
            .Select(item => item.Species)
            .Order()
            .ToArray();

        return Ok(new PredominantSpeciesResponseDto(cityName, species));
    }

    [HttpPost]
    public async Task<ActionResult<SettlementResponseDto>> Create(SettlementDto request)
    {
        if (IsInvalid(request)) return UnprocessableEntity();

        var worldName = await context.Worlds
            .Where(world => world.Id == request.WorldId)
            .Select(world => world.Name)
            .FirstOrDefaultAsync();
        if (worldName is null) return UnprocessableEntity();

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

        return CreatedAtAction(nameof(Get), new { settlement.Id }, ToResponse(settlement, worldName));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(SettlementDto request, Guid id)
    {
        if (IsInvalid(request)) return UnprocessableEntity();

        var worldName = await context.Worlds
            .Where(world => world.Id == request.WorldId)
            .Select(world => world.Name)
            .FirstOrDefaultAsync();
        if (worldName is null) return UnprocessableEntity();

        var settlement = await context.Settlements.FindAsync(id);
        if (settlement is null) return NotFound();

        settlement.Name = request.Name.Trim();
        settlement.Type = request.Type;
        settlement.Description = request.Description.Trim();
        settlement.Population = request.Population;
        settlement.WorldId = request.WorldId;

        await context.SaveChangesAsync();
        return Ok(ToResponse(settlement, worldName));
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

    private static SettlementResponseDto ToResponse(Settlement settlement, string worldName) =>
        new(settlement.Id, settlement.Name, settlement.Type, settlement.Description, settlement.Population, worldName);
}
