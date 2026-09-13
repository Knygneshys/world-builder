using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace backend.Data;

public class WorldBuilderContextFactory : IDesignTimeDbContextFactory<WorldBuilderContext>
{
    public WorldBuilderContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json")
            .Build();

        return new WorldBuilderContext(
            new DbContextOptionsBuilder<WorldBuilderContext>()
                .UseSqlServer(configuration.GetConnectionString("WorldBuilder"))
                .Options);
    }
}
