using ProjectBlu.Models;

namespace ProjectBlu.Dto;

public class ModelMapping : Profile
{
    public ModelMapping()
    {
        CreateMap<CreateNewsRequest, News>();
        CreateMap<CreateWikiArticleRequest, WikiArticle>();
        CreateMap<CreateWikiCategoryRequest, WikiCategory>();

        CreateMap<User, UserResponse>().ReverseMap();
        CreateMap<User, AuthorResponse>().ReverseMap();
        CreateMap<AuthorResponse, UserResponse>().ReverseMap();
        CreateMap<News, NewsResponse>().ReverseMap();
        CreateMap<WikiCategory, WikiCategoryResponse>().ReverseMap();
        CreateMap<WikiArticle, WikiArticleResponse>().ReverseMap();
    }
}
