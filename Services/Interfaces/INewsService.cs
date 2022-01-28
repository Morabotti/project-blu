namespace ProjectBlu.Services.Interfaces;

public interface INewsService
{
    Task<Response<NewsResponse>> CreateAsync(CreateNewsRequest request, UserResponse user);
    Task<Response<NewsResponse>> GetByIdAsync(int id);
    Task<Response<PaginationResponse<NewsResponse>>> GetManyAsync(PaginationQuery query);
    Task<Response<NewsResponse>> UpdateAsync(NewsResponse request, UserResponse user);
    Task<Response<NewsResponse>> DeleteAsync(int id, UserResponse user);
}
