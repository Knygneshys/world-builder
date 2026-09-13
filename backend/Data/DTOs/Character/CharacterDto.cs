using backend.Data.Entities.Enums;

namespace backend.Data.DTOs.Character;

public record CharacterDto(
    string Name,
    string? Alias,
    Species Species,
    int Age,
    Gender Gender,
    Alignment Alignment,
    string Description,
    Guid SettlementId);
