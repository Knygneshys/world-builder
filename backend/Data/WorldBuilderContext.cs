using backend.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class WorldBuilderContext(DbContextOptions<WorldBuilderContext> options) : IdentityDbContext(options)
{
    public DbSet<World> Worlds => Set<World>();
    public DbSet<Settlement> Settlements => Set<Settlement>();
    public DbSet<Character> Characters => Set<Character>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<World>()
            .HasOne(world => world.Creator)
            .WithMany()
            .HasForeignKey(world => world.CreatorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Settlement>()
            .HasOne(settlement => settlement.World)
            .WithMany(world => world.Settlements)
            .HasForeignKey(settlement => settlement.WorldId);

        modelBuilder.Entity<Character>()
            .HasOne(character => character.Settlement)
            .WithMany(settlement => settlement.Characters)
            .HasForeignKey(character => character.SettlementId);
    }
}
