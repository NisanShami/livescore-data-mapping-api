using LSports.DataMapping.Abstractions.Models.Requests.PeriodMappings;
using LSports.DataMapping.Abstractions.Models.Responses;
using LSports.DataMapping.Abstractions.Models.Responses.PeriodMappings;

namespace LSports.DataMapping.Abstractions.Interfaces;

public interface IPeriodMappingService
{
    Task<GetPeriodMappingsResponse> GetPeriodMappingsAsync(GetPeriodMappingsRequest request);
    Task<GetNotMappedPeriodsResponse> GetNotMappedPeriodsAsync(GetNotMappedPeriodsRequest request);
    Task<ResponseBase> CreatePeriodMappingAsync(CreatePeriodMappingRequest request);
    Task<ResponseBase> UpdatePeriodMappingAsync(int id, UpdatePeriodMappingRequest request);
    Task<ResponseBase> DeletePeriodMappingAsync(int id);
    Task<GetPeriodMappingsFiltersResponse> GetFiltersAsync(GetPeriodMappingsFiltersRequest request);
}
