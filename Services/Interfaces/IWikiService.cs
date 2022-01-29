namespace ProjectBlu.Services.Interfaces;

public interface IWikiService
{
    Task<Response<WikiCategoryResponse>> CreateWikiCategoryAsync(CreateWikiCategoryRequest request);
    Task<Response<WikiCategoryResponse>> GetWikiCategoryByIdAsync(int id, UserResponse user);
    Task<Response<List<WikiCategoryResponse>>> GetWikiCategoriesAsync(UserResponse user);
    Task<Response<WikiCategoryResponse>> UpdateWikiCategoryAsync(WikiCategoryResponse request);
    Task<Response<WikiCategoryResponse>> DeleteWikiCategoryAsync(int id);

    Task<Response<WikiArticleResponse>> CreateWikiArticleAsync(CreateWikiArticleRequest request, UserResponse user);
    Task<Response<WikiArticleResponse>> GetWikiArticleByIdAsync(int id, UserResponse user);
    Task<Response<WikiArticleResponse>> UpdateWikiArticleAsync(WikiArticleResponse request, UserResponse user);
    Task<Response<WikiArticleResponse>> DeleteWikiArticleAsync(int id, UserResponse user);
}
