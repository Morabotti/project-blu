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

    public async Task<Response<IssueCategoryResponse>> CreateCategoryAsync(
        CreateIssueCategoryRequest request
    )
    {
        throw new NotImplementedException();
    }

    public async Task<Response<IssueStatusResponse>> CreateStatusAsync(
        CreateIssueStatusRequest request
    )
    {
        throw new NotImplementedException();
    }

    public async Task<Response<IssueCategoryResponse>> DeleteCategoryAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<IssueStatusResponse>> DeleteStatusAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<List<IssueCategoryResponse>>> GetCategoriesAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Response<List<IssueStatusResponse>>> GetStatusesAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Response<IssueCategoryResponse>> UpdateCategoryAsync(
        IssueCategoryResponse request
    )
    {
        throw new NotImplementedException();
    }

    public async Task<Response<IssueStatusResponse>> UpdateStatusAsync(
        IssueStatusResponse request
    )
    {
        throw new NotImplementedException();
    }
}
