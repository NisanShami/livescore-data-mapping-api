using LSports.DataMapping.Abstractions.Interfaces;
using LSports.DataMapping.Abstractions.Models.Requests.PeriodMappings;
using LSports.DataMapping.Abstractions.Models.Responses;
using LSports.DataMapping.Abstractions.Models.Responses.PeriodMappings;
using Microsoft.Extensions.Logging;

namespace LSports.DataMapping.Services.Services;

public class PeriodMappingService : IPeriodMappingService
{
    private readonly IPeriodMappingRepository _repository;
    private readonly ILogger<PeriodMappingService> _logger;

    public PeriodMappingService(IPeriodMappingRepository repository, ILogger<PeriodMappingService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<GetPeriodMappingsResponse> GetPeriodMappingsAsync(GetPeriodMappingsRequest request)
    {
        try
        {
            var (data, totalCount) = await _repository.GetPeriodMappingsAsync(request);

            return new GetPeriodMappingsResponse
            {
                Data = data,
                TotalRecords = totalCount,
                Page = request.Page,
                PageSize = request.PageSize,
                Success = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting period mappings");
            return new GetPeriodMappingsResponse
            {
                Success = false,
                Errors = new List<string> { "An error occurred while retrieving period mappings" }
            };
        }
    }

    public async Task<GetNotMappedPeriodsResponse> GetNotMappedPeriodsAsync(GetNotMappedPeriodsRequest request)
    {
        try
        {
            var (data, totalCount) = await _repository.GetNotMappedPeriodsAsync(request);

            return new GetNotMappedPeriodsResponse
            {
                Data = data,
                TotalRecords = totalCount,
                Page = request.Page,
                PageSize = request.PageSize,
                Success = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting not mapped periods");
            return new GetNotMappedPeriodsResponse
            {
                Success = false,
                Errors = new List<string> { "An error occurred while retrieving not mapped periods" }
            };
        }
    }

    public async Task<ResponseBase> CreatePeriodMappingAsync(CreatePeriodMappingRequest request)
    {
        try
        {
            // Validate request
            if (string.IsNullOrEmpty(request.ProviderPeriod))
            {
                return new ResponseBase
                {
                    Success = false,
                    Errors = new List<string> { "Provider period is required" }
                };
            }

            if (request.LsportsPeriodId <= 0)
            {
                return new ResponseBase
                {
                    Success = false,
                    Errors = new List<string> { "LSports period ID is required" }
                };
            }

            var periodMapping = await _repository.CreatePeriodMappingAsync(request);

            return new ResponseBase
            {
                Success = true,
                Message = $"Period mapping created successfully with ID: {periodMapping.Id}"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating period mapping");
            return new ResponseBase
            {
                Success = false,
                Errors = new List<string> { "An error occurred while creating the period mapping" }
            };
        }
    }

    public async Task<ResponseBase> UpdatePeriodMappingAsync(int id, UpdatePeriodMappingRequest request)
    {
        try
        {
            // Validate request
            if (request.LsportsPeriodId <= 0)
            {
                return new ResponseBase
                {
                    Success = false,
                    Errors = new List<string> { "LSports period ID is required" }
                };
            }

            var periodMapping = await _repository.UpdatePeriodMappingAsync(id, request);
            if (periodMapping == null)
            {
                return new ResponseBase
                {
                    Success = false,
                    Errors = new List<string> { "Period mapping not found" }
                };
            }

            return new ResponseBase
            {
                Success = true,
                Message = "Period mapping updated successfully"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating period mapping with ID: {Id}", id);
            return new ResponseBase
            {
                Success = false,
                Errors = new List<string> { "An error occurred while updating the period mapping" }
            };
        }
    }

    public async Task<ResponseBase> DeletePeriodMappingAsync(int id)
    {
        try
        {
            var success = await _repository.DeletePeriodMappingAsync(id);
            if (!success)
            {
                return new ResponseBase
                {
                    Success = false,
                    Errors = new List<string> { "Period mapping not found" }
                };
            }

            return new ResponseBase
            {
                Success = true,
                Message = "Period mapping deleted successfully"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting period mapping with ID: {Id}", id);
            return new ResponseBase
            {
                Success = false,
                Errors = new List<string> { "An error occurred while deleting the period mapping" }
            };
        }
    }

    public async Task<GetPeriodMappingsFiltersResponse> GetFiltersAsync(GetPeriodMappingsFiltersRequest request)
    {
        try
        {
            var sports = await _repository.GetSportsAsync();
            var providers = await _repository.GetProvidersAsync();
            var leagues = await _repository.GetLeaguesAsync();

            return new GetPeriodMappingsFiltersResponse
            {
                Sports = sports,
                Providers = providers,
                Leagues = leagues,
                Success = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting filters");
            return new GetPeriodMappingsFiltersResponse
            {
                Success = false,
                Errors = new List<string> { "An error occurred while retrieving filters" }
            };
        }
    }
}
