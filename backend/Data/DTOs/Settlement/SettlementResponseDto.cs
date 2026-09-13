using backend.Data.Entities.Enums;

namespace backend.Data.DTOs.Settlement;

public record SettlementResponseDto(
    Guid Id,
    string Name,
    SettlementType Type,
    string Description,
    int Population,
    string WorldName);
