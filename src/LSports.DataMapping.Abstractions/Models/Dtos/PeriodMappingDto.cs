namespace LSports.DataMapping.Abstractions.Models.Dtos;

public class PeriodMappingDto
{
    public int Id { get; set; }
    public int ProviderId { get; set; }
    public string ProviderName { get; set; } = string.Empty;
    public int SportId { get; set; }
    public string SportName { get; set; } = string.Empty;
    public string ProviderPeriod { get; set; } = string.Empty;
    public int? LsportsPeriodId { get; set; }
    public string? LsportsPeriodName { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string UpdatedBy { get; set; } = string.Empty;
}

public class NotMappedPeriodDto
{
    public int Id { get; set; }
    public int ProviderId { get; set; }
    public string ProviderName { get; set; } = string.Empty;
    public int SportId { get; set; }
    public string SportName { get; set; } = string.Empty;
    public string ProviderPeriod { get; set; } = string.Empty;
}

public class SportDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class ProviderDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class FilterDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
