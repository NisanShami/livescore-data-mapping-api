using LSports.DataMapping.Abstractions.Models.DataBase;
using LSports.DataMapping.Abstractions.Models.Dtos;
using LSports.DataMapping.Abstractions.Models.Requests.PeriodMappings;

namespace LSports.DataMapping.Abstractions.Interfaces;

public interface IPeriodMappingRepository
{
    Task<(List<PeriodMappingDto> data, int totalCount)> GetPeriodMappingsAsync(GetPeriodMappingsRequest request);
    Task<(List<NotMappedPeriodDto> data, int totalCount)> GetNotMappedPeriodsAsync(GetNotMappedPeriodsRequest request);
    Task<PeriodMapping?> GetPeriodMappingByIdAsync(int id);
    Task<PeriodMapping> CreatePeriodMappingAsync(CreatePeriodMappingRequest request);
    Task<PeriodMapping?> UpdatePeriodMappingAsync(int id, UpdatePeriodMappingRequest request);
    Task<bool> DeletePeriodMappingAsync(int id);
    Task<List<FilterDto>> GetSportsAsync();
    Task<List<FilterDto>> GetProvidersAsync();
    Task<List<FilterDto>> GetLeaguesAsync();
}
