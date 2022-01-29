using ProjectBlu.Models;
using ProjectBlu.Repositories;
using ProjectBlu.Services.Interfaces;
using ProjectBlu.Utils;

namespace ProjectBlu.Services;

public class IssueService : IIssueService
{
    private readonly ProjectBluContext _context;
    private readonly IMapper _mapper;

    public IssueService(ProjectBluContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<IssueResponse>> CreateIssueAsync(
        CreateIssueRequest request,
        UserResponse user
    )
    {
        var issue = _mapper.Map<Issue>(request);

        issue.AuthorId = user.Id;

        _context.Issues.Add(issue);
        await _context.SaveChangesAsync();

        return new Response<IssueResponse>(
             _mapper.Map<IssueResponse>(issue)
        );
    }

    public async Task<Response<PaginationResponse<IssueResponse>>> GetIssuesAsync(
        PaginationQuery pagination,
        IssueQuery query
    )
    {
        // TODO: Add queries
        var issues = await _context.Issues
            .Include(i => i.Author)
            .Include(i => i.Assigned)
            .Include(i => i.Category)
            .Include(i => i.Status)
            .Include(i => i.Project)
            .OrderByDescending(order => order.CreatedAt)
            .Skip(pagination.Offset.GetValueOrDefault(0))
            .Take(pagination.Limit.GetValueOrDefault(20))
            .AsNoTracking()
            .ToListAsync();

        int count = await _context.Issues.CountAsync();

        return new Response<PaginationResponse<IssueResponse>>(
            new PaginationResponse<IssueResponse>(
                _mapper.Map<List<IssueResponse>>(issues),
                count
            )
        );
    }

    public async Task<Response<IssueResponse>> GetIssueByIdAsync(
        int id,
        UserResponse user
    )
    {
        var group = await GetUserGroupAsync(id, user.Id);

        if (group is null && user.Role != UserRole.Admin)
        {
            return new Response<IssueResponse>(null);
        }

        var issue = await _context.Issues
            .Where(i => i.Id == id)
            .Include(i => i.Author)
            .Include(i => i.Assigned)
            .Include(i => i.Category)
            .Include(i => i.Status)
            .Include(i => i.Project)
            .Include(i => i.TimeEntries.Where(x => user.Role == UserRole.Admin
                || x.UserId == user.Id
                || group.Permissions.Contains("showTimeEntry")))
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync();

        return new Response<IssueResponse>(
             _mapper.Map<IssueResponse>(issue)
        );
    }

    public async Task<Response<IssueResponse>> UpdateIssueAsync(
        IssueResponse request,
        UserResponse user
    )
    {
        // TODO: Create this method
        return await GetIssueByIdAsync(request.Id, user);
    }

    public async Task<Response<IssueResponse>> DeleteIssueAsync(int id)
    {
        var issue = await _context.Issues.FirstOrDefaultAsync(i => i.Id == id);

        if (issue is null)
        {
            return new Response<IssueResponse>(null);
        }

        _context.Issues.Remove(issue);
        await _context.SaveChangesAsync();

        return new Response<IssueResponse>("Operation successful", HttpStatusCode.OK);
    }

    public async Task<Response<IssueCategoryResponse>> CreateCategoryAsync(
        CreateIssueCategoryRequest request
    )
    {
        var category = _mapper.Map<IssueCategory>(request);

        _context.IssueCategories.Add(category);
        await _context.SaveChangesAsync();

        return new Response<IssueCategoryResponse>(
             _mapper.Map<IssueCategoryResponse>(category)
        );
    }

    public async Task<Response<IssueStatusResponse>> CreateStatusAsync(
        CreateIssueStatusRequest request
    )
    {
        var status = _mapper.Map<IssueStatus>(request);

        _context.IssueStatuses.Add(status);
        await _context.SaveChangesAsync();

        return new Response<IssueStatusResponse>(
             _mapper.Map<IssueStatusResponse>(status)
        );
    }

    public async Task<Response<List<IssueCategoryResponse>>> GetCategoriesAsync()
    {
        var categories = await _context.IssueCategories
            .AsNoTracking()
            .ToListAsync();

        return new Response<List<IssueCategoryResponse>>(
            _mapper.Map<List<IssueCategoryResponse>>(categories)
        );
    }

    public async Task<Response<List<IssueStatusResponse>>> GetStatusesAsync()
    {
        var statuses = await _context.IssueStatuses
            .AsNoTracking()
            .ToListAsync();

        return new Response<List<IssueStatusResponse>>(
            _mapper.Map<List<IssueStatusResponse>>(statuses)
        );
    }

    public async Task<Response<IssueCategoryResponse>> UpdateCategoryAsync(
        IssueCategoryResponse request
    )
    {
        var category = await _context.IssueCategories
            .FirstOrDefaultAsync(i => i.Id == request.Id);

        if (category is null)
        {
            return new Response<IssueCategoryResponse>(null);
        }

        UpdateMapper.MapValues(request, category);

        await _context.SaveChangesAsync();

        return new Response<IssueCategoryResponse>(
            _mapper.Map<IssueCategoryResponse>(category)
        );
    }

    public async Task<Response<IssueStatusResponse>> UpdateStatusAsync(
        IssueStatusResponse request
    )
    {
        var status = await _context.IssueStatuses
            .FirstOrDefaultAsync(i => i.Id == request.Id);

        if (status is null)
        {
            return new Response<IssueStatusResponse>(null);
        }

        UpdateMapper.MapValues(request, status);

        await _context.SaveChangesAsync();

        return new Response<IssueStatusResponse>(
            _mapper.Map<IssueStatusResponse>(status)
        );
    }

    public async Task<Response<IssueCategoryResponse>> DeleteCategoryAsync(int id)
    {
        var category = await _context.IssueCategories
            .FirstOrDefaultAsync(i => i.Id == id);

        if (category is null)
        {
            return new Response<IssueCategoryResponse>(null);
        }

        _context.IssueCategories.Remove(category);
        await _context.SaveChangesAsync();

        return new Response<IssueCategoryResponse>(
            "Operation successful",
            HttpStatusCode.OK
        );
    }

    public async Task<Response<IssueStatusResponse>> DeleteStatusAsync(int id)
    {
        var status = await _context.IssueStatuses.FirstOrDefaultAsync(i => i.Id == id);

        if (status is null)
        {
            return new Response<IssueStatusResponse>(null);
        }

        _context.IssueStatuses.Remove(status);
        await _context.SaveChangesAsync();

        return new Response<IssueStatusResponse>(
            "Operation successful",
            HttpStatusCode.OK
        );
    }

    private async Task<Group?> GetUserGroupAsync(int issueId, int userId)
    {
        Group? group = await _context.Members
            .Where(i => i.Project.Issues.Any(x => x.Id == issueId))
            .Where(i => i.UserId == userId)
            .Include(i => i.Project).ThenInclude(i => i.Issues)
            .Include(i => i.Group)
            .Select(i => i.Group)
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync();

        return group;
    }
}
