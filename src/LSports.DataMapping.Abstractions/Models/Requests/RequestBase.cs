namespace LSports.DataMapping.Abstractions.Models.Requests;

public abstract class RequestBase
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 50;
    public string? SortField { get; set; }
    public string? SortDirection { get; set; } = "asc";
}
