using backend.Data.Entities.Enums;

namespace backend.Data.Entities;

public class Settlement
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public SettlementType Type { get; set; }
    public required string Description { get; set; }
    public int Population { get; set; }
    public Guid WorldId { get; set; }
    public required World World { get; set; }
}
