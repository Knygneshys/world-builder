using backend.Entities.Enums;

namespace backend.Entities;

public class Character
{
    public required string Name { get; set; }
    public string? Alias { get; set; }
    public Species Species { get; set; }
    public int Age { get; set; }
    public Gender Gender { get; set; }
    public Alignment Alignment { get; set; }
    public required string Description { get; set; }
}
