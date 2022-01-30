using ProjectBlu.Models;
using ProjectBlu.Repositories;
using ProjectBlu.Services.Interfaces;
using ProjectBlu.Utils;

namespace ProjectBlu.Services;

public class ProjectService : IProjectService
{
    private readonly ProjectBluContext _context;
    private readonly IMapper _mapper;

    public ProjectService(ProjectBluContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<ProjectResponse>> CreateAsync(
        CreateProjectRequest request
    )
    {
        var project = _mapper.Map<Project>(request);

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        return new Response<ProjectResponse>(
            _mapper.Map<ProjectResponse>(project)
        );
    }

    public async Task<Response<List<ProjectResponse>>> GetManyAsync()
    {
        var projects = await _context.Projects
            .Include(i => i.Customer)
            .AsNoTracking()
            .ToListAsync();

        return new Response<List<ProjectResponse>>(
            _mapper.Map<List<ProjectResponse>>(projects)
        );
    }

    public async Task<Response<ProjectResponse>> GetByIdAsync(int id)
    {
        var project = await _context.Projects
            .Include(i => i.Customer)
            .Include(i => i.Members).ThenInclude(i => i.Group)
            .Include(i => i.Members).ThenInclude(i => i.User)
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync(i => i.Id == id);

        return new Response<ProjectResponse>(
            _mapper.Map<ProjectResponse>(project)
        );
    }

    public async Task<Response<ProjectResponse>> UpdateAsync(
        ProjectResponse request
    )
    {
        var group = await _context.Groups.FirstOrDefaultAsync(i => i.Id == request.Id);

        if (group is null)
        {
            return new Response<ProjectResponse>(null);
        }

        UpdateMapper.MapValues(request, group);

        await _context.SaveChangesAsync();

        return new Response<ProjectResponse>(
            _mapper.Map<ProjectResponse>(group)
        );
    }

    public async Task<Response<ProjectResponse>> DeleteAsync(int id)
    {
        var group = await _context.Groups.FirstOrDefaultAsync(i => i.Id == id);

        if (group is null)
        {
            return new Response<ProjectResponse>(null);
        }

        _context.Groups.Remove(group);
        await _context.SaveChangesAsync();

        return new Response<ProjectResponse>("Operation successful", HttpStatusCode.OK);
    }
}
