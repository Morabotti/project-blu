using ProjectBlu.Services.Interfaces;

namespace ProjectBlu.Controllers;

[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
[ApiController]
public class NewsController : ApiController
{
    private readonly INewsService _newsService;

    public NewsController(INewsService newsService)
    {
        _newsService = newsService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateNewsRequest request)
    {
        var response = await _newsService.CreateAsync(request, GetUserClaim());
        return HttpResponse(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetMany(
        [FromQuery] PaginationQuery paginationQuery
    )
    {
        var response = await _newsService.GetManyAsync(paginationQuery);
        return HttpResponse(response);
    }

    [HttpGet("{newsId}")]
    public async Task<IActionResult> GetById([FromRoute] int newsId)
    {
        var response = await _newsService.GetByIdAsync(newsId);
        return HttpResponse(response);
    }

    [HttpPut("{newsId}")]
    public async Task<IActionResult> Update([FromBody] NewsResponse request)
    {
        var response = await _newsService.UpdateAsync(request, GetUserClaim());
        return HttpResponse(response);
    }

    [HttpDelete("{newsId}")]
    public async Task<IActionResult> Delete([FromRoute] int newsId)
    {
        var response = await _newsService.DeleteAsync(newsId, GetUserClaim());
        return HttpResponse(response);
    }
}