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
        CreateMap<CreateIssueRequest, Issue>();
        CreateMap<CreateGroupRequest, Group>();
        CreateMap<CreateCustomerRequest, Customer>();
        CreateMap<CreateContactRequest, Contact>();
        CreateMap<CreateDealRequest, Deal>();
        CreateMap<CreateUserRequest, User>();
        CreateMap<CreateProjectRequest, Project>();

        CreateMap<User, UserResponse>()
            .ForMember(i => i.DecodeId, opt => opt.MapFrom(x => x.Id))
            .AfterMap<HashIdAction<User, UserResponse>>();
        CreateMap<UserResponse, User>().AfterMap<HashIdAction<UserResponse, User>>();

        CreateMap<User, AuthorResponse>().AfterMap<HashIdAction<User, AuthorResponse>>().ReverseMap();
        CreateMap<ImageAsset, UserImageResponse>().ReverseMap();
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
