using backend.Data.Entities.Enums;

namespace backend.Data.DTOs;

public record CharacterCreateDto(
    string Name,
    string? Alias,
    Species Species,
    int Age,
    Gender Gender,
    Alignment Alignment,
    string Description);
