using Microsoft.AspNetCore.Identity;

namespace backend.Data.Entities;

public class World
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string CreatorId { get; set; }
    public IdentityUser Creator { get; set; } = null!;
    public ICollection<Settlement> Settlements { get; set; } = [];
}
