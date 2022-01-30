using ProjectBlu.Services.Interfaces;

namespace ProjectBlu.Controllers;

[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
[ApiController]
public class ProjectController : ApiController
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpPost, Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateProjectRequest request)
    {
        var response = await _projectService.CreateAsync(request);
        return HttpResponse(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetMany()
    {
        var response = await _projectService.GetManyAsync();
        return HttpResponse(response);
    }

    [HttpGet("{projectId}")]
    public async Task<IActionResult> GetById([FromRoute] int projectId)
    {
        var response = await _projectService.GetByIdAsync(projectId);
        return HttpResponse(response);
    }

    [HttpPut("{projectId}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update([FromBody] ProjectResponse request)
    {
        var response = await _projectService.UpdateAsync(request);
        return HttpResponse(response);
    }

    [HttpDelete("{projectId}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete([FromRoute] int projectId)
    {
        var response = await _projectService.DeleteAsync(projectId);
        return HttpResponse(response);
    }
}