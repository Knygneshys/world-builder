using backend.Data.Entities.Enums;
using Microsoft.AspNetCore.Identity;

namespace backend.Data.Seeding;

public class UserSeeder : ISeeder
{
    public async Task SeedAsync(WorldBuilderContext context, IServiceProvider services)
    {
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        var users = new[]
        {
            (Username: "WorldAdmin", Email: "world.admin@gmail.com", Password: "5891Sdfjgka!lakg", Role: Roles.Admin),
            (Username: "TestPlayer1", Email: "test.player1@example.com", Password: "ForestTrail1!", Role: Roles.Player),
            (Username: "TestPlayer2", Email: "test.player2@example.com", Password: "MountainPeak2!", Role: Roles.Player),
            (Username: "TestPlayer3", Email: "test.player3@example.com", Password: "OceanBreeze3!", Role: Roles.Player)
        };

        foreach (var seed in users)
        {
            var user = await userManager.FindByNameAsync(seed.Username);
            if (user is null)
            {
                user = new IdentityUser
                {
                    UserName = seed.Username,
                    Email = seed.Email,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, seed.Password);
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(
                        $"Failed to seed user '{seed.Username}': {string.Join(", ", result.Errors.Select(error => error.Description))}");
                }
            }

            var roleName = seed.Role.ToString();
            if (await userManager.IsInRoleAsync(user, roleName)) continue;

            var roleResult = await userManager.AddToRoleAsync(user, roleName);
            if (!roleResult.Succeeded)
            {
                throw new InvalidOperationException(
                    $"Failed to assign role '{roleName}' to user '{seed.Username}': {string.Join(", ", roleResult.Errors.Select(error => error.Description))}");
            }
        }
    }
}
