using ProjectBlu.Services.Interfaces;

namespace ProjectBlu.Controllers;

[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
[ApiController]
public class DealController : ApiController
{
    private readonly IDealService _dealService;

    public DealController(IDealService dealService)
    {
        _dealService = dealService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDealRequest request)
    {
        var response = await _dealService.CreateAsync(request);
        return HttpResponse(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetMany(
        [FromQuery] DealQuery query,
        [FromQuery] PaginationQuery pagination
    )
    {
        var response = await _dealService.GetManyAsync(query, pagination);
        return HttpResponse(response);
    }

    [HttpGet("{dealId}")]
    public async Task<IActionResult> GetById([FromRoute] int dealId)
    {
        var response = await _dealService.GetByIdAsync(dealId);
        return HttpResponse(response);
    }

    [HttpPut("{dealId}")]
    public async Task<IActionResult> Update([FromBody] DealResponse request)
    {
        var response = await _dealService.UpdateAsync(request, GetUserClaim());
        return HttpResponse(response);
    }

    [HttpDelete("{dealId}")]
    public async Task<IActionResult> Delete([FromRoute] int dealId)
    {
        var response = await _dealService.DeleteAsync(dealId, GetUserClaim());
        return HttpResponse(response);
    }
}