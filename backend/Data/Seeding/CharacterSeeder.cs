using backend.Data.Entities;
using backend.Data.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Seeding;

public class CharacterSeeder : ISeeder
{
    public async Task SeedAsync(WorldBuilderContext context, IServiceProvider services)
    {
        if (await context.Characters.AnyAsync()) return;

        var settlementId = await context.Settlements
            .Select(settlement => (Guid?)settlement.Id)
            .FirstOrDefaultAsync();
        if (settlementId is null) return;

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
                Description = "A veteran guardian who protects the northern roads from raiders."
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
                Description = "A quiet ranger who maps forgotten paths through ancient forests."
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
                Description = "A meticulous smith whose blades are prized across the realm."
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
                Description = "A cheerful courier who slips through borders no army can cross."
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
                Description = "A disciplined knight seeking redemption for his fallen clan."
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
                Description = "An excitable inventor whose devices work almost as often as intended."
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
                Description = "A wandering healer who serves villages overlooked by the crown."
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
                Description = "A reserved caravan guard known for keeping every promise."
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
                Description = "A sharp-tongued chronicler who exposes corrupt nobles."
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
                Description = "A calculating guild treasurer who values order above friendship."
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
                Description = "An aloof astrologer who reads omens in the movement of distant stars."
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
                Description = "A renowned brewer who offers shelter to travelers in need."
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
                Description = "A riverboat gambler with a talent for escaping impossible debts."
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
                Description = "A patient magistrate who settles disputes in the mountain provinces."
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
                Description = "A practical engineer who builds pumps and mills for poor settlements."
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
                Description = "A court musician who trades secrets to the highest bidder."
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
                Description = "A former arena fighter who now frees captives and hunted outcasts."
            }
        ];

        foreach (var character in characters) character.SettlementId = settlementId.Value;
        context.Characters.AddRange(characters);

        await context.SaveChangesAsync();
    }
}
