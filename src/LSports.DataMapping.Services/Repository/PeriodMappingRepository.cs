using LSports.DataMapping.Abstractions.Interfaces;
using LSports.DataMapping.Abstractions.Models.DataBase;
using LSports.DataMapping.Abstractions.Models.Dtos;
using LSports.DataMapping.Abstractions.Models.Requests.PeriodMappings;
using LSports.DataMapping.Services.Data;
using Microsoft.EntityFrameworkCore;

namespace LSports.DataMapping.Services.Repository;

public class PeriodMappingRepository : IPeriodMappingRepository
{
    private readonly DataMappingDbContext _context;

    public PeriodMappingRepository(DataMappingDbContext context)
    {
        _context = context;
    }

    public async Task<(List<PeriodMappingDto> data, int totalCount)> GetPeriodMappingsAsync(GetPeriodMappingsRequest request)
    {
        var query = _context.PeriodMappings
            .Where(pm => pm.IsActive && pm.LsportsPeriodId.HasValue)
            .AsQueryable();

        // Apply filters
        if (request.SportIds?.Any() == true)
            query = query.Where(pm => request.SportIds.Contains(pm.SportId));

        if (request.ProviderIds?.Any() == true)
            query = query.Where(pm => request.ProviderIds.Contains(pm.ProviderId));

        if (request.LastUpdateFrom.HasValue)
            query = query.Where(pm => pm.UpdatedDate >= request.LastUpdateFrom.Value);

        if (request.LastUpdateTo.HasValue)
            query = query.Where(pm => pm.UpdatedDate <= request.LastUpdateTo.Value);

        if (!string.IsNullOrEmpty(request.SearchText))
        {
            var searchText = request.SearchText.ToLower();
            query = query.Where(pm => 
                pm.ProviderPeriod.ToLower().Contains(searchText) ||
                pm.LsportsPeriodName!.ToLower().Contains(searchText) ||
                pm.ProviderName.ToLower().Contains(searchText) ||
                pm.SportName.ToLower().Contains(searchText));
        }

        // Apply sorting
        if (!string.IsNullOrEmpty(request.SortField))
        {
            query = request.SortField.ToLower() switch
            {
                "sportname" => request.SortDirection?.ToLower() == "desc" 
                    ? query.OrderByDescending(pm => pm.SportName)
                    : query.OrderBy(pm => pm.SportName),
                "providername" => request.SortDirection?.ToLower() == "desc"
                    ? query.OrderByDescending(pm => pm.ProviderName)
                    : query.OrderBy(pm => pm.ProviderName),
                "providerperiod" => request.SortDirection?.ToLower() == "desc"
                    ? query.OrderByDescending(pm => pm.ProviderPeriod)
                    : query.OrderBy(pm => pm.ProviderPeriod),
                "lsportsperiodname" => request.SortDirection?.ToLower() == "desc"
                    ? query.OrderByDescending(pm => pm.LsportsPeriodName)
                    : query.OrderBy(pm => pm.LsportsPeriodName),
                "updateddate" => request.SortDirection?.ToLower() == "desc"
                    ? query.OrderByDescending(pm => pm.UpdatedDate)
                    : query.OrderBy(pm => pm.UpdatedDate),
                _ => query.OrderBy(pm => pm.Id)
            };
        }
        else
        {
            query = query.OrderBy(pm => pm.Id);
        }

        var totalCount = await query.CountAsync();

        var data = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(pm => new PeriodMappingDto
            {
                Id = pm.Id,
                ProviderId = pm.ProviderId,
                ProviderName = pm.ProviderName,
                SportId = pm.SportId,
                SportName = pm.SportName,
                ProviderPeriod = pm.ProviderPeriod,
                LsportsPeriodId = pm.LsportsPeriodId,
                LsportsPeriodName = pm.LsportsPeriodName,
                IsActive = pm.IsActive,
                CreatedDate = pm.CreatedDate,
                UpdatedDate = pm.UpdatedDate,
                UpdatedBy = pm.UpdatedBy
            })
            .ToListAsync();

        return (data, totalCount);
    }

    public async Task<(List<NotMappedPeriodDto> data, int totalCount)> GetNotMappedPeriodsAsync(GetNotMappedPeriodsRequest request)
    {
        var query = _context.PeriodMappings
            .Where(pm => pm.IsActive && !pm.LsportsPeriodId.HasValue)
            .AsQueryable();

        // Apply filters
        if (request.SportIds?.Any() == true)
            query = query.Where(pm => request.SportIds.Contains(pm.SportId));

        if (request.ProviderIds?.Any() == true)
            query = query.Where(pm => request.ProviderIds.Contains(pm.ProviderId));

        if (!string.IsNullOrEmpty(request.SearchText))
        {
            var searchText = request.SearchText.ToLower();
            query = query.Where(pm => 
                pm.ProviderPeriod.ToLower().Contains(searchText) ||
                pm.ProviderName.ToLower().Contains(searchText) ||
                pm.SportName.ToLower().Contains(searchText));
        }

        query = query.OrderBy(pm => pm.Id);

        var totalCount = await query.CountAsync();

        var data = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(pm => new NotMappedPeriodDto
            {
                Id = pm.Id,
                ProviderId = pm.ProviderId,
                ProviderName = pm.ProviderName,
                SportId = pm.SportId,
                SportName = pm.SportName,
                ProviderPeriod = pm.ProviderPeriod
            })
            .ToListAsync();

        return (data, totalCount);
    }

    public async Task<PeriodMapping?> GetPeriodMappingByIdAsync(int id)
    {
        return await _context.PeriodMappings
            .FirstOrDefaultAsync(pm => pm.Id == id && pm.IsActive);
    }

    public async Task<PeriodMapping> CreatePeriodMappingAsync(CreatePeriodMappingRequest request)
    {
        var periodMapping = new PeriodMapping
        {
            ProviderId = request.ProviderId,
            ProviderName = request.ProviderName,
            SportId = request.SportId,
            SportName = request.SportName,
            ProviderPeriod = request.ProviderPeriod,
            LsportsPeriodId = request.LsportsPeriodId,
            LsportsPeriodName = request.LsportsPeriodName,
            UpdatedBy = request.UpdatedBy,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        };

        _context.PeriodMappings.Add(periodMapping);
        await _context.SaveChangesAsync();

        return periodMapping;
    }

    public async Task<PeriodMapping?> UpdatePeriodMappingAsync(int id, UpdatePeriodMappingRequest request)
    {
        var periodMapping = await GetPeriodMappingByIdAsync(id);
        if (periodMapping == null)
            return null;

        periodMapping.LsportsPeriodId = request.LsportsPeriodId;
        periodMapping.LsportsPeriodName = request.LsportsPeriodName;
        periodMapping.UpdatedBy = request.UpdatedBy;
        periodMapping.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return periodMapping;
    }

    public async Task<bool> DeletePeriodMappingAsync(int id)
    {
        var periodMapping = await GetPeriodMappingByIdAsync(id);
        if (periodMapping == null)
            return false;

        periodMapping.IsActive = false;
        periodMapping.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<FilterDto>> GetSportsAsync()
    {
        return await _context.PeriodMappings
            .Where(pm => pm.IsActive)
            .Select(pm => new { pm.SportId, pm.SportName })
            .Distinct()
            .Select(s => new FilterDto { Id = s.SportId, Name = s.SportName })
            .OrderBy(s => s.Name)
            .ToListAsync();
    }

    public async Task<List<FilterDto>> GetProvidersAsync()
    {
        return await _context.PeriodMappings
            .Where(pm => pm.IsActive)
            .Select(pm => new { pm.ProviderId, pm.ProviderName })
            .Distinct()
            .Select(p => new FilterDto { Id = p.ProviderId, Name = p.ProviderName })
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<List<FilterDto>> GetLeaguesAsync()
    {
        // For now, return empty list as leagues are not implemented yet
        return new List<FilterDto>();
    }
}
