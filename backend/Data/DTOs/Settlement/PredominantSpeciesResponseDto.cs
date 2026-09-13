using backend.Data.Entities.Enums;

namespace backend.Data.DTOs.Settlement;

public record PredominantSpeciesResponseDto(string CityName, Species[] Species);
