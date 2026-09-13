using backend.Data.Entities.Enums;

namespace backend.Data.DTOs;

public record SettlementDto(
    string Name,
    SettlementType Type,
    string Description,
    int Population,
    Guid WorldId);
