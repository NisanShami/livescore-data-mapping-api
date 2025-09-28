using LSports.DataMapping.Abstractions.Models.Dtos;

namespace LSports.DataMapping.Abstractions.Models.Responses.PeriodMappings;

public class GetPeriodMappingsResponse : PagedResponseBase<PeriodMappingDto>
{
}

public class GetNotMappedPeriodsResponse : PagedResponseBase<NotMappedPeriodDto>
{
}

public class GetPeriodMappingsFiltersResponse : ResponseBase
{
    public List<FilterDto> Sports { get; set; } = new();
    public List<FilterDto> Providers { get; set; } = new();
    public List<FilterDto> Leagues { get; set; } = new();
}
