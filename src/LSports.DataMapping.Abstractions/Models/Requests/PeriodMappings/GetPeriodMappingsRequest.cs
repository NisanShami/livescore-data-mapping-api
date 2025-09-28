namespace LSports.DataMapping.Abstractions.Models.Requests.PeriodMappings;

public class GetPeriodMappingsRequest : RequestBase
{
    public List<int>? SportIds { get; set; }
    public List<int>? ProviderIds { get; set; }
    public List<int>? LeagueIds { get; set; }
    public DateTime? LastUpdateFrom { get; set; }
    public DateTime? LastUpdateTo { get; set; }
    public string? SearchText { get; set; }
}

public class GetNotMappedPeriodsRequest : RequestBase
{
    public List<int>? SportIds { get; set; }
    public List<int>? ProviderIds { get; set; }
    public string? SearchText { get; set; }
}

public class CreatePeriodMappingRequest
{
    public int ProviderId { get; set; }
    public string ProviderName { get; set; } = string.Empty;
    public int SportId { get; set; }
    public string SportName { get; set; } = string.Empty;
    public string ProviderPeriod { get; set; } = string.Empty;
    public int LsportsPeriodId { get; set; }
    public string LsportsPeriodName { get; set; } = string.Empty;
    public string UpdatedBy { get; set; } = string.Empty;
}

public class UpdatePeriodMappingRequest
{
    public int LsportsPeriodId { get; set; }
    public string LsportsPeriodName { get; set; } = string.Empty;
    public string UpdatedBy { get; set; } = string.Empty;
}

public class GetPeriodMappingsFiltersRequest
{
    // Empty for now, can be extended later
}
