namespace backend.Data.Seeding;

public class ApplicationSeeder(IEnumerable<ISeeder> seeders)
{
    public async Task SeedAllAsync(WorldBuilderContext context, IServiceProvider services)
    {
        foreach (var seeder in seeders)
        {
            await seeder.SeedAsync(context, services);
        }
    }
}
