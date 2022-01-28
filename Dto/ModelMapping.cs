using ProjectBlu.Models;

namespace ProjectBlu.Dto;

public class ModelMapping : Profile
{
    public ModelMapping()
    {
        CreateMap<CreateNewsRequest, News>();

        CreateMap<User, UserResponse>().ReverseMap();
        CreateMap<User, AuthorResponse>().ReverseMap();
        CreateMap<AuthorResponse, UserResponse>().ReverseMap();
        CreateMap<News, NewsResponse>().ReverseMap();
    }
}
