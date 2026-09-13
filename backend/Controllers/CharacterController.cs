using backend.Data;
using backend.Data.DTOs;
using backend.Data.Entities;
using backend.Data.Entities.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController]
[Route("api/characters")]
public class CharacterController(WorldBuilderContext context) : ControllerBase
{
    private const int PageSize = 10;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Character>>> List(
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

        var query = context.Characters.AsNoTracking();

        if (species.HasValue) query = query.Where(character => character.Species == species);
        if (gender.HasValue) query = query.Where(character => character.Gender == gender);
        if (alignment.HasValue) query = query.Where(character => character.Alignment == alignment);

        return Ok(await query
            .OrderBy(character => character.Name)
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Character>> Get(Guid id)
    {
        var character = await context.Characters.AsNoTracking()
            .FirstOrDefaultAsync(character => character.Id == id);

        return character is null ? NotFound() : Ok(character);
    }

    [HttpPost]
    public async Task<ActionResult<Character>> Create(CharacterDto request)
    {
        if (IsInvalid(request)) return UnprocessableEntity();

        var character = new Character
        {
            Id = Guid.NewGuid(),
            Name = request.Name.Trim(),
            Alias = request.Alias,
            Species = request.Species,
            Age = request.Age,
            Gender = request.Gender,
            Alignment = request.Alignment,
            Description = request.Description.Trim()
        };

        context.Characters.Add(character);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { character.Id }, character);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(CharacterDto request, Guid id)
    {
        if (IsInvalid(request)) return UnprocessableEntity();

        var character = await context.Characters.FindAsync(id);
        if (character is null) return NotFound();

        character.Name = request.Name.Trim();
        character.Alias = request.Alias;
        character.Species = request.Species;
        character.Age = request.Age;
        character.Gender = request.Gender;
        character.Alignment = request.Alignment;
        character.Description = request.Description.Trim();

        await context.SaveChangesAsync();
        return Ok(character);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var character = await context.Characters.FindAsync(id);
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
}
