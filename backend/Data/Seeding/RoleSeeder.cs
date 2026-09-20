using backend.Data.Entities.Enums;
using Microsoft.AspNetCore.Identity;

namespace backend.Data.Seeding;

public class RoleSeeder : ISeeder
{
    public async Task SeedAsync(WorldBuilderContext context, IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        foreach (var roleName in Enum.GetNames<Roles>())
        {
            if (await roleManager.RoleExistsAsync(roleName)) continue;

            var result = await roleManager.CreateAsync(new IdentityRole(roleName));
            if (!result.Succeeded)
            {
                throw new InvalidOperationException(
                    $"Failed to seed role '{roleName}': {string.Join(", ", result.Errors.Select(error => error.Description))}");
            }
        }
    }
}
