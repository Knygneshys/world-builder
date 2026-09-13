using backend.Data.Entities;
using backend.Data.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Seeding;

public class CharacterSeeder : ISeeder
{
    public async Task SeedAsync(WorldBuilderContext context, IServiceProvider services)
    {
        if (await context.Characters.AnyAsync()) return;

        var settlements = await context.Settlements.ToListAsync();
        var skyholdId = new Guid("92cd7639-fd42-4003-b09b-d94fdbf70be5");
        
        Character[] characters =
        [
            new Character
            {
                Id = new Guid("56e301cf-1db1-4808-a6e3-1b43f9513953"),
                Name = "Aelric Stormward",
                Alias = "The Grey Sentinel",
                Species = Species.Human,
                Age = 42,
                Gender = Gender.Male,
                Alignment = Alignment.LawfulGood,
                Description = "A veteran guardian who protects the northern roads from raiders.",
                SettlementId = skyholdId
            },
            new Character
            {
                Id = new Guid("89060273-075a-43af-82cd-c07dc0bd8e68"),
                Name = "Liora Moonleaf",
                Alias = "Whisper",
                Species = Species.Elf,
                Age = 126,
                Gender = Gender.Female,
                Alignment = Alignment.NeutralGood,
                Description = "A quiet ranger who maps forgotten paths through ancient forests.",
                SettlementId = skyholdId
            },
            new Character
            {
                Id = new Guid("29f41994-fde0-441b-9410-d0e793682cfa"),
                Name = "Bromm Ironvein",
                Alias = null,
                Species = Species.Dwarf,
                Age = 88,
                Gender = Gender.Male,
                Alignment = Alignment.LawfulNeutral,
                Description = "A meticulous smith whose blades are prized across the realm.",
                SettlementId = skyholdId
            },
            new Character
            {
                Id = new Guid("e3f265f6-7e17-441e-9b0f-acfb9f63fe82"),
                Name = "Pippa Underbough",
                Alias = "Quickstep",
                Species = Species.Halfling,
                Age = 31,
                Gender = Gender.Female,
                Alignment = Alignment.ChaoticGood,
                Description = "A cheerful courier who slips through borders no army can cross.",
                SettlementId = skyholdId
            },
            new Character
            {
                Id = new Guid("614fc658-50ef-4fb6-81f1-2d8bb691ac20"),
                Name = "Rhogar Embercrest",
                Alias = "Ashborn",
                Species = Species.Dragonborn,
                Age = 37,
                Gender = Gender.Male,
                Alignment = Alignment.LawfulGood,
                Description = "A disciplined knight seeking redemption for his fallen clan.",
                SettlementId = skyholdId
            },
            new Character
            {
                Id = new Guid("79497856-192d-4c43-902a-00ca3b99095f"),
                Name = "Nim Fizzlespark",
                Alias = null,
                Species = Species.Gnome,
                Age = 64,
                Gender = Gender.Androgynous,
                Alignment = Alignment.ChaoticNeutral,
                Description = "An excitable inventor whose devices work almost as often as intended.",
                SettlementId =  settlements[1].Id
            },
            new Character
            {
                Id = new Guid("ac5a547d-db2e-4d84-8972-4124a0abfe56"),
                Name = "Seren Vale",
                Alias = "Dawnwalker",
                Species = Species.HalfElf,
                Age = 54,
                Gender = Gender.Female,
                Alignment = Alignment.NeutralGood,
                Description = "A wandering healer who serves villages overlooked by the crown.",
                SettlementId =  settlements[1].Id
            },
            new Character
            {
                Id = new Guid("8c20abe8-17d3-40f4-a035-dbed301212bb"),
                Name = "Garruk Stonejaw",
                Alias = null,
                Species = Species.HalfOrc,
                Age = 29,
                Gender = Gender.Male,
                Alignment = Alignment.TrueNeutral,
                Description = "A reserved caravan guard known for keeping every promise.",
                SettlementId =  settlements[1].Id
            },
            new Character
            {
                Id = new Guid("3a693f63-f06d-4d89-b104-d3120d177b03"),
                Name = "Vespera Nox",
                Alias = "The Red Quill",
                Species = Species.Tiefling,
                Age = 33,
                Gender = Gender.Female,
                Alignment = Alignment.ChaoticGood,
                Description = "A sharp-tongued chronicler who exposes corrupt nobles.",
                SettlementId =  settlements[1].Id
            },
            new Character
            {
                Id = new Guid("64b7df9e-d381-490c-aee1-d9cfaf522a77"),
                Name = "Cedric Marr",
                Alias = "Coinmaster",
                Species = Species.Human,
                Age = 51,
                Gender = Gender.Male,
                Alignment = Alignment.LawfulNeutral,
                Description = "A calculating guild treasurer who values order above friendship.",
                SettlementId =  settlements[1].Id
            },
            new Character
            {
                Id = new Guid("9ae0992e-8210-4ad2-afc7-ed0f73d09197"),
                Name = "Thalanil Starbloom",
                Alias = null,
                Species = Species.Elf,
                Age = 203,
                Gender = Gender.Male,
                Alignment = Alignment.TrueNeutral,
                Description = "An aloof astrologer who reads omens in the movement of distant stars.",
                SettlementId =  settlements[1].Id
            },
            new Character
            {
                Id = new Guid("a44b971a-d116-4760-8992-3cbf1d5396c4"),
                Name = "Hilda Copperkeg",
                Alias = "Auntie Hilda",
                Species = Species.Dwarf,
                Age = 117,
                Gender = Gender.Female,
                Alignment = Alignment.NeutralGood,
                Description = "A renowned brewer who offers shelter to travelers in need.",
                SettlementId =  settlements[2].Id
            },
            new Character
            {
                Id = new Guid("ac75fdea-c25f-46f6-bd7f-e7a612d6a8d0"),
                Name = "Tobin Reed",
                Alias = "Lucky",
                Species = Species.Halfling,
                Age = 45,
                Gender = Gender.Male,
                Alignment = Alignment.ChaoticNeutral,
                Description = "A riverboat gambler with a talent for escaping impossible debts.",
                SettlementId =  settlements[2].Id
            },
            new Character
            {
                Id = new Guid("ec5112ed-6efb-452f-99e5-61262697e1f0"),
                Name = "Kavax Frostscale",
                Alias = null,
                Species = Species.Dragonborn,
                Age = 61,
                Gender = Gender.Androgynous,
                Alignment = Alignment.LawfulNeutral,
                Description = "A patient magistrate who settles disputes in the mountain provinces.",
                SettlementId =  settlements[2].Id
            },
            new Character
            {
                Id = new Guid("7a090382-4587-46e2-a802-801ce85e20e7"),
                Name = "Orla Cogwhistle",
                Alias = "Tinker",
                Species = Species.Gnome,
                Age = 79,
                Gender = Gender.Female,
                Alignment = Alignment.LawfulGood,
                Description = "A practical engineer who builds pumps and mills for poor settlements.",
                SettlementId =  settlements[2].Id
            },
            new Character
            {
                Id = new Guid("8402a24d-00cb-40d1-9cbd-4c515c1bfa63"),
                Name = "Marek Duskhollow",
                Alias = "Gravesong",
                Species = Species.HalfElf,
                Age = 72,
                Gender = Gender.Male,
                Alignment = Alignment.NeutralEvil,
                Description = "A court musician who trades secrets to the highest bidder.",
                SettlementId =  settlements[3].Id
            },
            new Character
            {
                Id = new Guid("6f1553fa-5887-4f37-a7dd-bae9349d4f08"),
                Name = "Zara Bloodmark",
                Alias = "The Unbound",
                Species = Species.HalfOrc,
                Age = 36,
                Gender = Gender.Female,
                Alignment = Alignment.ChaoticGood,
                Description = "A former arena fighter who now frees captives and hunted outcasts.",
                SettlementId =  settlements[3].Id
            },
            new Character
            {
                Id = new Guid("287ca66c-83c8-4502-8d8d-12f33bc343a3"),
                Name = "Ilyra Ashveil",
                Alias = "Cinder Seer",
                Species = Species.Tiefling,
                Age = 46,
                Gender = Gender.Female,
                Alignment = Alignment.TrueNeutral,
                Description = "A fire-reader who warns travelers when the ash fields are about to shift.",
                SettlementId =  settlements[3].Id
            },
            new Character
            {
                Id = new Guid("bb8a83fd-46ba-4897-98ad-bf1f1871f666"),
                Name = "Doran Flint",
                Alias = null,
                Species = Species.Human,
                Age = 58,
                Gender = Gender.Male,
                Alignment = Alignment.LawfulGood,
                Description = "A watch captain who keeps the fortress gates open to refugees.",
                SettlementId =  settlements[3].Id
            },
            new Character
            {
                Id = new Guid("a9902f1b-0ae1-46f9-bf15-fab2043d592c"),
                Name = "Elaris Prismheart",
                Alias = "Brightsong",
                Species = Species.Elf,
                Age = 164,
                Gender = Gender.Androgynous,
                Alignment = Alignment.NeutralGood,
                Description = "A crystal singer who repairs fractures in the forest with resonant hymns.",
                SettlementId =  settlements[4].Id
            },
            new Character
            {
                Id = new Guid("ccdf42f5-8c28-4a5b-a121-e39588043edb"),
                Name = "Mira Vell",
                Alias = "Silverhand",
                Species = Species.Human,
                Age = 39,
                Gender = Gender.Female,
                Alignment = Alignment.ChaoticNeutral,
                Description = "A diplomat who carries unofficial messages between rival moonlit courts.",
                SettlementId = skyholdId
            },
            new Character
            {
                Id = new Guid("bfb9827e-ab01-433b-bc35-f5427118ef10"),
                Name = "Korren Mossback",
                Alias = null,
                Species = Species.Dwarf,
                Age = 44,
                Gender = Gender.Male,
                Alignment = Alignment.TrueNeutral,
                Description = "A marsh guide who can find firm ground where maps show only water.",
                SettlementId = skyholdId
            },
            new Character
            {
                Id = new Guid("a34d8918-a57d-4c99-95b9-7eed74aca059"),
                Name = "Tessa Brassbolt",
                Alias = "Sparks",
                Species = Species.Gnome,
                Age = 71,
                Gender = Gender.Female,
                Alignment = Alignment.ChaoticGood,
                Description = "A railway mechanic who turns discarded machine parts into rescue tools.",
                SettlementId = skyholdId
            }
        ];

        context.Characters.AddRange(characters);
        await context.SaveChangesAsync();
    }
}
