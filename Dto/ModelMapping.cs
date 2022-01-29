using ProjectBlu.Models;
using ProjectBlu.Models.Owned;

namespace ProjectBlu.Dto;

public class ModelMapping : Profile
{
    public ModelMapping()
    {
        CreateMap<CreateNewsRequest, News>();
        CreateMap<CreateWikiArticleRequest, WikiArticle>();
        CreateMap<CreateWikiCategoryRequest, WikiCategory>();
        CreateMap<CreateIssueCategoryRequest, IssueCategory>();
        CreateMap<CreateIssueStatusRequest, IssueStatus>();

        CreateMap<User, UserResponse>().ReverseMap();
        CreateMap<User, AuthorResponse>().ReverseMap();
        CreateMap<AuthorResponse, UserResponse>().ReverseMap();
        CreateMap<News, NewsResponse>().ReverseMap();
        CreateMap<WikiCategory, WikiCategoryResponse>().ReverseMap();
        CreateMap<WikiArticle, WikiArticleResponse>().ReverseMap();
        CreateMap<IssueCategory, IssueCategoryResponse>().ReverseMap();
        CreateMap<IssueStatus, IssueStatusResponse>().ReverseMap();
        CreateMap<Contact, ContactResponse>().ReverseMap();
        CreateMap<Customer, CustomerResponse>().ReverseMap();
        CreateMap<Deal, DealResponse>().ReverseMap();
        CreateMap<Document, DocumentResponse>().ReverseMap();
        CreateMap<Group, GroupResponse>().ReverseMap();
        CreateMap<Issue, IssueResponse>().ReverseMap();
        CreateMap<Location, LocationResponse>().ReverseMap();
        CreateMap<Member, MemberResponse>().ReverseMap();
        CreateMap<Project, ProjectResponse>().ReverseMap();
        CreateMap<TimeEntry, TimeEntryResponse>().ReverseMap();
    }
}
