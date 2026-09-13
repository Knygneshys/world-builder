using backend.Data.Entities.Enums;

namespace backend.Data.DTOs.Settlement;

public record SettlementDto(
    string Name,
    SettlementType Type,
    string Description,
    int Population,
    Guid WorldId);
