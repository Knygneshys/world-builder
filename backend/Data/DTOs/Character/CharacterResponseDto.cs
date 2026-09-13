using backend.Data.Entities.Enums;

namespace backend.Data.DTOs.Character;

public record CharacterResponseDto(
    Guid Id,
    string Name,
    string? Alias,
    Species Species,
    int Age,
    Gender Gender,
    Alignment Alignment,
    string Description,
    string SettlementName);
