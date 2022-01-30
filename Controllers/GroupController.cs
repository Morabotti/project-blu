using ProjectBlu.Services.Interfaces;

namespace ProjectBlu.Controllers;

[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
[ApiController]
public class GroupController : ApiController
{
    private readonly IGroupService _groupService;

    public GroupController(IGroupService groupService)
    {
        _groupService = groupService;
    }

    [HttpPost, Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateGroupRequest request)
    {
        var response = await _groupService.CreateAsync(request);
        return HttpResponse(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetMany()
    {
        var response = await _groupService.GetManyAsync();
        return HttpResponse(response);
    }

    [HttpGet("{groupId}")]
    public async Task<IActionResult> GetById([FromRoute] int groupId)
    {
        var response = await _groupService.GetByIdAsync(groupId);
        return HttpResponse(response);
    }

    [HttpPut("{groupId}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update([FromBody] GroupResponse request)
    {
        var response = await _groupService.UpdateAsync(request);
        return HttpResponse(response);
    }

    [HttpDelete("{groupId}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete([FromRoute] int groupId)
    {
        var response = await _groupService.DeleteAsync(groupId);
        return HttpResponse(response);
    }
}