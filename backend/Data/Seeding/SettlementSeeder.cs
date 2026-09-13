using backend.Data.Entities;
using backend.Data.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Seeding;

public class SettlementSeeder : ISeeder
{
    public async Task SeedAsync(WorldBuilderContext context, IServiceProvider services)
    {
        if (await context.Settlements.AnyAsync()) return;

        context.Settlements.AddRange(
            new Settlement
            {
                Id = new Guid("92cd7639-fd42-4003-b09b-d94fdbf70be5"),
                Name = "Skyhold",
                Type = SettlementType.City,
                Description = "A busy city built across three linked floating islands.",
                Population = 48000,
                WorldId = new Guid("3b724df1-2453-401b-9c32-f29adc6c9431"),
                World = null!
            },
            new Settlement
            {
                Id = new Guid("2ac6bbdc-aea7-4100-8b2c-9205c1f8f8f6"),
                Name = "Cinderwatch",
                Type = SettlementType.Fortress,
                Description = "A basalt stronghold guarding the safest road through the ash fields.",
                Population = 9200,
                WorldId = new Guid("22df67b6-3d89-4564-975d-e171be5c63eb"),
                World = null!
            },
            new Settlement
            {
                Id = new Guid("602ea5d0-7811-4c79-a96c-a27ec45c20a2"),
                Name = "Crystalhaven",
                Type = SettlementType.Town,
                Description = "Artisans shape the singing crystal harvested from the nearby forest.",
                Population = 13800,
                WorldId = new Guid("ba17c434-2ec3-40e0-96fc-7b8691954a13"),
                World = null!
            },
            new Settlement
            {
                Id = new Guid("61d482d0-e55a-40db-9276-0fc9df8e40c1"),
                Name = "Moonfall",
                Type = SettlementType.City,
                Description = "Silver lanterns illuminate a city shared uneasily by two empires.",
                Population = 67500,
                WorldId = new Guid("ad5c888a-7933-4503-beef-b3577b43233c"),
                World = null!
            },
            new Settlement
            {
                Id = new Guid("e3fe0605-57e1-438c-9113-e917afe0b0c1"),
                Name = "Stormrest",
                Type = SettlementType.Outpost,
                Description = "A sheltered landing station for ships crossing the eternal storm.",
                Population = 740,
                WorldId = new Guid("3b724df1-2453-401b-9c32-f29adc6c9431"),
                World = null!
            },
            new Settlement
            {
                Id = new Guid("ae17db91-a7ef-4b14-8d89-c28ae6d3acb9"),
                Name = "Cloudspire",
                Type = SettlementType.Metropolis,
                Description = "A vertical capital whose towers rise above the highest clouds.",
                Population = 185000,
                WorldId = new Guid("3b724df1-2453-401b-9c32-f29adc6c9431"),
                World = null!
            },
            new Settlement
            {
                Id = new Guid("c3813857-9b63-4c3e-b9bb-bec884eca026"),
                Name = "Emberdeep",
                Type = SettlementType.City,
                Description = "A city warmed and powered by channels of slow-moving magma.",
                Population = 35600,
                WorldId = new Guid("22df67b6-3d89-4564-975d-e171be5c63eb"),
                World = null!
            },
            new Settlement
            {
                Id = new Guid("b6944f82-f436-4d71-8c28-292aaab306d5"),
                Name = "Blackforge",
                Type = SettlementType.Town,
                Description = "Renowned smiths work volcanic glass in this soot-covered town.",
                Population = 11400,
                WorldId = new Guid("22df67b6-3d89-4564-975d-e171be5c63eb"),
                World = null!
            },
            new Settlement
            {
                Id = new Guid("13a4acce-448a-41eb-8ac6-fbd257b40047"),
                Name = "Larksong",
                Type = SettlementType.Village,
                Description = "A quiet woodland village whose homes grow within crystal trees.",
                Population = 1900,
                WorldId = new Guid("ba17c434-2ec3-40e0-96fc-7b8691954a13"),
                World = null!
            },
            new Settlement
            {
                Id = new Guid("33a2a853-b7c6-4d8c-bc1e-0f14766fe24d"),
                Name = "Prism Gate",
                Type = SettlementType.Fortress,
                Description = "Refracted light shields this fortress at the forest's eastern edge.",
                Population = 7600,
                WorldId = new Guid("ba17c434-2ec3-40e0-96fc-7b8691954a13"),
                World = null!
            },
            new Settlement
            {
                Id = new Guid("5a472d42-5637-4a9c-9a40-569b7a9d4b7a"),
                Name = "Eventide",
                Type = SettlementType.Metropolis,
                Description = "The western empire's crowded capital glows beneath a violet moon.",
                Population = 210000,
                WorldId = new Guid("ad5c888a-7933-4503-beef-b3577b43233c"),
                World = null!
            },
            new Settlement
            {
                Id = new Guid("e0cb98bd-6c02-47e9-b805-29c5fcd62b90"),
                Name = "Silverfen",
                Type = SettlementType.Village,
                Description = "Reed farmers live beside pools that mirror the moonlit sky.",
                Population = 2600,
                WorldId = new Guid("ad5c888a-7933-4503-beef-b3577b43233c"),
                World = null!
            },
            new Settlement
            {
                Id = new Guid("c2447dbc-88a0-4af6-a442-d196cd06b0e5"),
                Name = "Greenwake",
                Type = SettlementType.Village,
                Description = "Pilgrims and farmers share a valley watched by ancient stone faces.",
                Population = 3200,
                WorldId = new Guid("fbaef98c-7605-4ee7-8aa7-5276e7869dce"),
                World = null!
            },
            new Settlement
            {
                Id = new Guid("3ac3e862-c22e-4a9b-9c95-864baaaf7261"),
                Name = "Temple's Rest",
                Type = SettlementType.Town,
                Description = "A market town built among the courtyards of a ruined temple.",
                Population = 9800,
                WorldId = new Guid("fbaef98c-7605-4ee7-8aa7-5276e7869dce"),
                World = null!
            },
            new Settlement
            {
                Id = new Guid("3a34dd95-02dc-4f93-8c71-99fdade4c6b5"),
                Name = "Icebarrow",
                Type = SettlementType.Fortress,
                Description = "Thick ice walls protect the northern clans from wandering giants.",
                Population = 6800,
                WorldId = new Guid("b3c5b661-07f0-4319-876b-e01976f51db5"),
                World = null!
            },
            new Settlement
            {
                Id = new Guid("73236c6f-b416-400b-be45-aa54d5446e4b"),
                Name = "Aurora Camp",
                Type = SettlementType.Outpost,
                Description = "Hunters gather beneath the aurora before crossing the frozen wastes.",
                Population = 560,
                WorldId = new Guid("b3c5b661-07f0-4319-876b-e01976f51db5"),
                World = null!
            },
            new Settlement
            {
                Id = new Guid("5a6f3a42-b119-4cc3-99c3-72b3a5339fa0"),
                Name = "Nightward",
                Type = SettlementType.City,
                Description = "A walled district-city occupying the mountain's lower terraces.",
                Population = 52200,
                WorldId = new Guid("65cb3147-b6f6-4d96-a325-f92d491c89d0"),
                World = null!
            },
            new Settlement
            {
                Id = new Guid("79eec20d-f69d-45bf-8f19-362d81f9e6ae"),
                Name = "Ravenstep",
                Type = SettlementType.Town,
                Description = "Messengers and traders crowd this steep town of black stairways.",
                Population = 12700,
                WorldId = new Guid("65cb3147-b6f6-4d96-a325-f92d491c89d0"),
                World = null!
            },
            new Settlement
            {
                Id = new Guid("87e64ac8-b4a2-45f9-93e0-999b89477880"),
                Name = "Mirelight",
                Type = SettlementType.Village,
                Description = "Lanterns on tall poles guide travelers through the surrounding marsh.",
                Population = 2200,
                WorldId = new Guid("8e24e5a9-b636-4bed-82e5-d31e93a5e157"),
                World = null!
            },
            new Settlement
            {
                Id = new Guid("204371be-7809-452d-9c55-ee5467c86ae8"),
                Name = "Sunken Crown",
                Type = SettlementType.Outpost,
                Description = "Divers search a half-submerged palace from this wooden outpost.",
                Population = 430,
                WorldId = new Guid("8e24e5a9-b636-4bed-82e5-d31e93a5e157"),
                World = null!
            },
            new Settlement
            {
                Id = new Guid("c95e0752-9b62-4be1-acf7-25dbe1f9fdc9"),
                Name = "Brasswood",
                Type = SettlementType.Town,
                Description = "Mechanics repair expedition machines beside the metallic jungle.",
                Population = 15100,
                WorldId = new Guid("08e8b266-c350-43aa-913d-5c20b45ece7f"),
                World = null!
            },
            new Settlement
            {
                Id = new Guid("be85cf8d-1c22-4dd9-9bc4-8e4bda6b85a2"),
                Name = "Gearford",
                Type = SettlementType.City,
                Description = "Factories line the river where the frontier railway begins.",
                Population = 43800,
                WorldId = new Guid("08e8b266-c350-43aa-913d-5c20b45ece7f"),
                World = null!
            },
            new Settlement
            {
                Id = new Guid("3952ad10-7326-4bb9-802b-ff239e320c1f"),
                Name = "Starwell",
                Type = SettlementType.Metropolis,
                Description = "A radiant capital surrounds the world's largest celestial tower.",
                Population = 245000,
                WorldId = new Guid("cc2ab8f6-3566-41a0-b83e-382184636111"),
                World = null!
            },
            new Settlement
            {
                Id = new Guid("9018e2e3-9b91-47b9-81d8-5c9c5b712059"),
                Name = "Beacon's End",
                Type = SettlementType.Outpost,
                Description = "Astronomers keep a lonely observatory beyond the settled plains.",
                Population = 310,
                WorldId = new Guid("cc2ab8f6-3566-41a0-b83e-382184636111"),
                World = null!
            },
            new Settlement
            {
                Id = new Guid("04586966-c582-4cce-8d38-56ef15303cd6"),
                Name = "Sandveil",
                Type = SettlementType.Town,
                Description = "Caravans gather where shifting dunes reveal an ancient gateway.",
                Population = 10600,
                WorldId = new Guid("7ef8f9bf-6f9c-4700-a445-f16a9f9ddf52"),
                World = null!
            },
            new Settlement
            {
                Id = new Guid("18c42bda-e3f8-434c-8167-9e36dd89ff21"),
                Name = "Equinox",
                Type = SettlementType.City,
                Description = "Delegates from four seasonal realms meet in this neutral city.",
                Population = 58300,
                WorldId = new Guid("4eec5b03-807f-40ca-b746-1a04ea6d62eb"),
                World = null!
            },
            new Settlement
            {
                Id = new Guid("cf9872e8-1dfb-452d-8b10-7b56423d6f1d"),
                Name = "Leviathan's Rest",
                Type = SettlementType.City,
                Description = "A sprawling harbor shelters fleets hunting the creatures of the deep.",
                Population = 71400,
                WorldId = new Guid("b0f10129-e088-43b9-a3ab-f19b161a0f0a"),
                World = null!
            });

        await context.SaveChangesAsync();
    }
}
