namespace LSports.DataMapping.Abstractions.Models.Responses;

public class ResponseBase
{
    public bool Success { get; set; } = true;
    public List<string> Errors { get; set; } = new();
    public string? Message { get; set; }
}

public class PagedResponseBase<T> : ResponseBase
{
    public List<T> Data { get; set; } = new();
    public int TotalRecords { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalRecords / PageSize) : 0;
}
