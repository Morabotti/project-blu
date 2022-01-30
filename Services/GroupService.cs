using ProjectBlu.Models;
using ProjectBlu.Repositories;
using ProjectBlu.Services.Interfaces;
using ProjectBlu.Utils;

namespace ProjectBlu.Services;

public class GroupService : IGroupService
{
    private readonly ProjectBluContext _context;
    private readonly IMapper _mapper;

    public GroupService(ProjectBluContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<GroupResponse>> CreateAsync(
        CreateGroupRequest request
    )
    {
        var group = _mapper.Map<Group>(request);

        _context.Groups.Add(group);
        await _context.SaveChangesAsync();

        return new Response<GroupResponse>(
            _mapper.Map<GroupResponse>(group)
        );
    }

    public async Task<Response<List<GroupResponse>>> GetManyAsync()
    {
        var groups = await _context.Groups
            .OrderBy(g => g.Position)
            .AsNoTracking()
            .ToListAsync();

        return new Response<List<GroupResponse>>(
            _mapper.Map<List<GroupResponse>>(groups)
        );
    }

    public async Task<Response<GroupResponse>> GetByIdAsync(int id)
    {
        var group = await _context.Groups
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id);

        return new Response<GroupResponse>(
            _mapper.Map<GroupResponse>(group)
        );
    }

    public async Task<Response<GroupResponse>> UpdateAsync(
        GroupResponse request
    )
    {
        var group = await _context.Groups.FirstOrDefaultAsync(i => i.Id == request.Id);

        if (group is null)
        {
            return new Response<GroupResponse>(null);
        }

        UpdateMapper.MapValues(request, group);

        await _context.SaveChangesAsync();

        return new Response<GroupResponse>(
            _mapper.Map<GroupResponse>(group)
        );
    }

    public async Task<Response<GroupResponse>> DeleteAsync(int id)
    {
        var group = await _context.Groups.FirstOrDefaultAsync(i => i.Id == id);

        if (group is null)
        {
            return new Response<GroupResponse>(null);
        }

        _context.Groups.Remove(group);
        await _context.SaveChangesAsync();

        return new Response<GroupResponse>("Operation successful", HttpStatusCode.OK);
    }
}
