using backend.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class WorldBuilderContext(DbContextOptions<WorldBuilderContext> options) : DbContext(options)
{
    public DbSet<World> Worlds => Set<World>();
    public DbSet<Settlement> Settlements => Set<Settlement>();
    public DbSet<Character> Characters => Set<Character>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Settlement>()
            .HasOne(settlement => settlement.World)
            .WithMany(world => world.Settlements)
            .HasForeignKey(settlement => settlement.WorldId);
    }
}
