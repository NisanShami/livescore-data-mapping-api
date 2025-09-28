using LSports.DataMapping.Abstractions.Interfaces;
using LSports.DataMapping.Abstractions.Models.Requests.PeriodMappings;
using LSports.DataMapping.Abstractions.Models.Responses;
using LSports.DataMapping.Abstractions.Models.Responses.PeriodMappings;
using Microsoft.AspNetCore.Mvc;

namespace LSports.DataMapping.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PeriodMappingsController(IPeriodMappingService periodMappingService) : ControllerBase
{

    [HttpPost("[action]")]
    public async Task<ActionResult<GetPeriodMappingsResponse>> GetMapped([FromBody] GetPeriodMappingsRequest request)
    {
        var response = await periodMappingService.GetPeriodMappingsAsync(request);
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<GetNotMappedPeriodsResponse>> GetNotMapped([FromBody] GetNotMappedPeriodsRequest request)
    {
        var response = await periodMappingService.GetNotMappedPeriodsAsync(request);
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<ResponseBase>> Create([FromBody] CreatePeriodMappingRequest request)
    {
        var response = await periodMappingService.CreatePeriodMappingAsync(request);
        return Ok(response);
    }

    [HttpPut("[action]/{id:int}")]
    public async Task<ActionResult<ResponseBase>> Update(int id, [FromBody] UpdatePeriodMappingRequest request)
    {
        var response = await periodMappingService.UpdatePeriodMappingAsync(id, request);
        return Ok(response);
    }

    [HttpDelete("[action]/{id:int}")]
    public async Task<ActionResult<ResponseBase>> Delete(int id)
    {
        var response = await periodMappingService.DeletePeriodMappingAsync(id);
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<GetPeriodMappingsFiltersResponse>> GetFilters([FromBody] GetPeriodMappingsFiltersRequest request)
    {
        var response = await periodMappingService.GetFiltersAsync(request);
        return Ok(response);
    }
}
