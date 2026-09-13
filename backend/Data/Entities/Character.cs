using backend.Data.Entities.Enums;

namespace backend.Data.Entities;

public class Character
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Alias { get; set; }
    public Species Species { get; set; }
    public int Age { get; set; }
    public Gender Gender { get; set; }
    public Alignment Alignment { get; set; }
    public required string Description { get; set; }
}
