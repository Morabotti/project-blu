using ProjectBlu.Services.Interfaces;

namespace ProjectBlu.Controllers;

[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
[ApiController]
public class IssueController : ApiController
{
    private readonly IIssueService _issueService;

    public IssueController(IIssueService issueService)
    {
        _issueService = issueService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateIssue([FromBody] CreateIssueRequest request)
    {
        var response = await _issueService.CreateIssueAsync(request, GetUserClaim());
        return HttpResponse(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetIssues(
        [FromQuery] IssueQuery query,
        [FromQuery] PaginationQuery pagination
    )
    {
        var response = await _issueService.GetIssuesAsync(pagination, query);
        return HttpResponse(response);
    }

    [HttpGet("{issueId}")]
    public async Task<IActionResult> GetIssueById([FromRoute] int issueId)
    {
        var response = await _issueService.GetIssueByIdAsync(issueId, GetUserClaim());
        return HttpResponse(response);
    }

    [HttpPut("{issueId}")]
    public async Task<IActionResult> UpdateIssue([FromBody] IssueResponse request)
    {
        var response = await _issueService.UpdateIssueAsync(request, GetUserClaim());
        return HttpResponse(response);
    }

    [HttpDelete("{issueId}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteIssue([FromRoute] int issueId)
    {
        var response = await _issueService.DeleteIssueAsync(issueId);
        return HttpResponse(response);
    }

    [HttpPost("status"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateStatus([FromBody] CreateIssueStatusRequest request)
    {
        var response = await _issueService.CreateStatusAsync(request);
        return HttpResponse(response);
    }

    [HttpGet("status")]
    public async Task<IActionResult> GetStatuses()
    {
        var response = await _issueService.GetStatusesAsync();
        return HttpResponse(response);
    }

    [HttpPut("status/{statusId}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateStatus([FromBody] IssueStatusResponse request)
    {
        var response = await _issueService.UpdateStatusAsync(request);
        return HttpResponse(response);
    }

    [HttpDelete("status/{statusId}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteStatus([FromRoute] int statusId)
    {
        var response = await _issueService.DeleteStatusAsync(statusId);
        return HttpResponse(response);
    }

    [HttpPost("category"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateCategory([FromBody] CreateIssueCategoryRequest request)
    {
        var response = await _issueService.CreateCategoryAsync(request);
        return HttpResponse(response);
    }

    [HttpGet("category")]
    public async Task<IActionResult> GetCategories()
    {
        var response = await _issueService.GetCategoriesAsync();
        return HttpResponse(response);
    }

    [HttpPut("category/{categoryId}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateCategory([FromBody] IssueCategoryResponse request)
    {
        var response = await _issueService.UpdateCategoryAsync(request);
        return HttpResponse(response);
    }

    [HttpDelete("category/{categoryId}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteCategory([FromRoute] int categoryId)
    {
        var response = await _issueService.DeleteCategoryAsync(categoryId);
        return HttpResponse(response);
    }
}