using backend.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Seeding;

public class WorldSeeder : ISeeder
{
    public async Task SeedAsync(WorldBuilderContext context, IServiceProvider services)
    {
        if (await context.Worlds.AnyAsync()) return;

        var creators = await context.Users.Take(2).ToListAsync();
        var creatorId1 = creators[0].Id;
        var creatorId2 = creators[1].Id;

        context.Worlds.AddRange(
            new World
            {
                Id = new Guid("3b724df1-2453-401b-9c32-f29adc6c9431"),
                CreatorId = creatorId1,
                Name = "Aetheria",
                Description = "Floating islands drift above an endless storm, linked by ancient skyways."
            },
            new World
            {
                Id = new Guid("22df67b6-3d89-4564-975d-e171be5c63eb"),
                CreatorId = creatorId2,
                Name = "Ashenfall",
                Description = "Volcanic kingdoms endure beneath a sky darkened by perpetual ash."
            },
            new World
            {
                Id = new Guid("ba17c434-2ec3-40e0-96fc-7b8691954a13"),
                CreatorId = creatorId1,
                Name = "Caelora",
                Description = "Crystal forests sing with magic across a bright and untamed wilderness."
            },
            new World
            {
                Id = new Guid("ad5c888a-7933-4503-beef-b3577b43233c"),
                CreatorId = creatorId2,
                Name = "Duskreach",
                Description = "Twilight never ends in a realm divided between rival moonlit empires."
            },
            new World
            {
                Id = new Guid("fbaef98c-7605-4ee7-8aa7-5276e7869dce"),
                CreatorId = creatorId2,
                Name = "Eldervale",
                Description = "Old gods sleep beneath green valleys dotted with ruined temples."
            },
            new World
            {
                Id = new Guid("b3c5b661-07f0-4319-876b-e01976f51db5"),
                CreatorId = creatorId2,
                Name = "Frostholm",
                Description = "Clans and colossal beasts contest a frozen continent under dancing auroras."
            },
            new World
            {
                Id = new Guid("65cb3147-b6f6-4d96-a325-f92d491c89d0"),
                CreatorId = creatorId1,
                Name = "Gloamspire",
                Description = "A vast gothic city climbs a mountain where daylight cannot reach."
            },
            new World
            {
                Id = new Guid("8e24e5a9-b636-4bed-82e5-d31e93a5e157"),
                CreatorId = creatorId2,
                Name = "Hollowmere",
                Description = "Mist-covered marshes conceal drowned cities and restless spirits."
            },
            new World
            {
                Id = new Guid("08e8b266-c350-43aa-913d-5c20b45ece7f"),
                CreatorId = creatorId1,
                Name = "Ironwild",
                Description = "Steam-driven frontier towns push into jungles filled with metal beasts."
            },
            new World
            {
                Id = new Guid("cc2ab8f6-3566-41a0-b83e-382184636111"),
                CreatorId = creatorId2,
                Name = "Luminaris",
                Description = "Scholars harness living starlight in cities built around celestial towers."
            },
            new World
            {
                Id = new Guid("7ef8f9bf-6f9c-4700-a445-f16a9f9ddf52"),
                CreatorId = creatorId2,
                Name = "Nethervane",
                Description = "Shifting desert winds uncover gateways to forgotten dimensions."
            },
            new World
            {
                Id = new Guid("4eec5b03-807f-40ca-b746-1a04ea6d62eb"),
                CreatorId = creatorId1,
                Name = "Solstice",
                Description = "Seasons rule four neighboring realms locked in an uneasy alliance."
            },
            new World
            {
                Id = new Guid("b0f10129-e088-43b9-a3ab-f19b161a0f0a"),
                CreatorId = creatorId1,
                Name = "Tidehaven",
                Description = "Island nations sail a boundless ocean haunted by leviathans and lost fleets."
            });

        await context.SaveChangesAsync();
    }
}
