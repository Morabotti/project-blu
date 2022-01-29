namespace ProjectBlu.Services.Interfaces;

public interface IIssueService
{
    Task<Response<IssueResponse>> CreateIssueAsync(CreateIssueRequest request, UserResponse user);
    Task<Response<PaginationResponse<IssueResponse>>> GetIssuesAsync(PaginationQuery pagination, IssueQuery query);
    Task<Response<IssueResponse>> GetIssueByIdAsync(int id, UserResponse user);
    Task<Response<IssueResponse>> UpdateIssueAsync(IssueResponse request, UserResponse user);
    Task<Response<IssueResponse>> DeleteIssueAsync(int id);

    Task<Response<IssueCategoryResponse>> CreateCategoryAsync(CreateIssueCategoryRequest request);
    Task<Response<List<IssueCategoryResponse>>> GetCategoriesAsync();
    Task<Response<IssueCategoryResponse>> UpdateCategoryAsync(IssueCategoryResponse request);
    Task<Response<IssueCategoryResponse>> DeleteCategoryAsync(int id);

    Task<Response<IssueStatusResponse>> CreateStatusAsync(CreateIssueStatusRequest request);
    Task<Response<List<IssueStatusResponse>>> GetStatusesAsync();
    Task<Response<IssueStatusResponse>> UpdateStatusAsync(IssueStatusResponse request);
    Task<Response<IssueStatusResponse>> DeleteStatusAsync(int id);
}
