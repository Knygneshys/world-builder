namespace backend.Data.Seeding;

public interface ISeeder
{
    Task SeedAsync(WorldBuilderContext context, IServiceProvider services);
}
