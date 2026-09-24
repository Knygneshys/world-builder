using System.Security.Claims;
using backend.Auth;
using backend.Data;
using backend.Data.DTOs.Character;
using backend.Data.Entities;
using backend.Data.Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Authorize]
[ApiController]
[Route("api/characters")]
public class CharacterController(WorldBuilderContext context) : ControllerBase
{
    private const int PageSize = 10;

    private IQueryable<Character> AccessibleCharacters => context.Characters
        .Where(character => AuthUtils.IsAdmin(User) ||
            character.Settlement.World.CreatorId == User.FindFirstValue(ClaimTypes.NameIdentifier));

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CharacterResponseDto>>> List(
        [FromQuery] Species? species,
        [FromQuery] Gender? gender,
        [FromQuery] Alignment? alignment,
        [FromQuery] int page = 1)
    {
        if (page < 1 ||
            species.HasValue && !Enum.IsDefined(species.Value) ||
            gender.HasValue && !Enum.IsDefined(gender.Value) ||
            alignment.HasValue && !Enum.IsDefined(alignment.Value))
        {
            return BadRequest();
        }

        var query = AccessibleCharacters.AsNoTracking();

        if (species.HasValue) query = query.Where(character => character.Species == species);
        if (gender.HasValue) query = query.Where(character => character.Gender == gender);
        if (alignment.HasValue) query = query.Where(character => character.Alignment == alignment);

        return Ok(await query
            .OrderBy(character => character.Name)
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .Select(character => new CharacterResponseDto(
                character.Id,
                character.Name,
                character.Alias,
                character.Species,
                character.Age,
                character.Gender,
                character.Alignment,
                character.Description,
                character.Settlement.Name))
            .ToListAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CharacterResponseDto>> Get(Guid id)
    {
        var character = await AccessibleCharacters
            .Where(character => character.Id == id)
            .Select(character => new CharacterResponseDto(
                character.Id,
                character.Name,
                character.Alias,
                character.Species,
                character.Age,
                character.Gender,
                character.Alignment,
                character.Description,
                character.Settlement.Name))
            .FirstOrDefaultAsync();

        return character is null ? NotFound() : Ok(character);
    }

    [HttpPost]
    public async Task<ActionResult<CharacterResponseDto>> Create(CharacterDto request)
    {
        if (IsInvalid(request)) return UnprocessableEntity();

        var settlementName = await context.Settlements
            .Where(settlement => settlement.Id == request.SettlementId &&
                (AuthUtils.IsAdmin(User) || settlement.World.CreatorId == User.FindFirstValue(ClaimTypes.NameIdentifier)))
            .Select(settlement => settlement.Name)
            .FirstOrDefaultAsync();
        if (settlementName is null) return UnprocessableEntity("Can't create a character without a settlement!");

        var character = new Character
        {
            Id = Guid.NewGuid(),
            Name = request.Name.Trim(),
            Alias = request.Alias,
            Species = request.Species,
            Age = request.Age,
            Gender = request.Gender,
            Alignment = request.Alignment,
            Description = request.Description.Trim(),
            SettlementId = request.SettlementId
        };

        context.Characters.Add(character);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { character.Id }, ToResponse(character, settlementName));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(CharacterDto request, Guid id)
    {
        if (IsInvalid(request)) return UnprocessableEntity();

        var settlementName = await context.Settlements
            .Where(settlement => settlement.Id == request.SettlementId &&
                (AuthUtils.IsAdmin(User) || settlement.World.CreatorId == User.FindFirstValue(ClaimTypes.NameIdentifier)))
            .Select(settlement => settlement.Name)
            .FirstOrDefaultAsync();
        if (settlementName is null) return UnprocessableEntity("Can't update a character without a settlement!");

        var character = await AccessibleCharacters.FirstOrDefaultAsync(character => character.Id == id);
        if (character is null) return NotFound();

        character.Name = request.Name.Trim();
        character.Alias = request.Alias;
        character.Species = request.Species;
        character.Age = request.Age;
        character.Gender = request.Gender;
        character.Alignment = request.Alignment;
        character.Description = request.Description.Trim();
        character.SettlementId = request.SettlementId;

        await context.SaveChangesAsync();
        return Ok(ToResponse(character, settlementName));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var character = await AccessibleCharacters.FirstOrDefaultAsync(character => character.Id == id);
        if (character is null) return NotFound();

        context.Characters.Remove(character);
        await context.SaveChangesAsync();
        return NoContent();
    }

    private static bool IsInvalid(CharacterDto request) =>
        string.IsNullOrWhiteSpace(request.Name) ||
        string.IsNullOrWhiteSpace(request.Description) ||
        request.Age < 0 ||
        !Enum.IsDefined(request.Species) ||
        !Enum.IsDefined(request.Gender) ||
        !Enum.IsDefined(request.Alignment);

    private static CharacterResponseDto ToResponse(Character character, string settlementName) =>
        new(
            character.Id,
            character.Name,
            character.Alias,
            character.Species,
            character.Age,
            character.Gender,
            character.Alignment,
            character.Description,
            settlementName);
}
