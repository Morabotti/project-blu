using ProjectBlu.Models;
using ProjectBlu.Repositories;
using ProjectBlu.Services.Interfaces;
using ProjectBlu.Utils;

namespace ProjectBlu.Services;

public class WikiService : IWikiService
{
    private readonly ProjectBluContext _context;
    private readonly IMapper _mapper;

    public WikiService(ProjectBluContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<WikiArticleResponse>> CreateWikiArticleAsync(
        CreateWikiArticleRequest request,
        UserResponse user
    )
    {
        var article = _mapper.Map<WikiArticle>(request);
        article.AuthorId = user.DecodeId;

        _context.WikiArticles.Add(article);
        await _context.SaveChangesAsync();

        var response = _mapper.Map<WikiArticleResponse>(article);
        response.Author = _mapper.Map<AuthorResponse>(user);

        return new Response<WikiArticleResponse>(response);
    }

    public async Task<Response<WikiCategoryResponse>> CreateWikiCategoryAsync(
        CreateWikiCategoryRequest request
    )
    {
        var category = _mapper.Map<WikiCategory>(request);

        _context.WikiCategories.Add(category);
        await _context.SaveChangesAsync();

        return new Response<WikiCategoryResponse>(
            _mapper.Map<WikiCategoryResponse>(category)
        );
    }

    public async Task<Response<WikiArticleResponse>> GetWikiArticleByIdAsync(
        int id,
        UserResponse user
    )
    {
        var article = await _context.WikiArticles
            .Where(i => i.Id == id)
            .Where(i => i.AuthorId == user.DecodeId || i.IsPublic || user.Role == UserRole.Admin)
            .Include(i => i.Category)
            .Include(i => i.Author)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return new Response<WikiArticleResponse>(
            _mapper.Map<WikiArticleResponse>(article)
        );
    }

    public async Task<Response<List<WikiCategoryResponse>>> GetWikiCategoriesAsync(UserResponse user)
    {
        var categories = await _context.WikiCategories
            .Select(i => new WikiCategoryResponse
            {
                Id = i.Id,
                Title = i.Title,
                IsPublic = i.IsPublic,
                Tags = i.Tags,
                Description = i.Description,
                ArticleCount = i.Articles
                    .Where(p => p.IsPublic || user.Role == UserRole.Admin || p.AuthorId == user.DecodeId)
                    .Count(),
            })
            .AsNoTracking()
            .ToListAsync();

        return new Response<List<WikiCategoryResponse>>(categories);
    }

    public async Task<Response<WikiCategoryResponse>> GetWikiCategoryByIdAsync(
        int id,
        UserResponse user
    )
    {
        var category = await _context.WikiCategories
            .Where(i => i.Id == id)
            .Include(i => i.Articles
                .Where(a => (a.AuthorId == user.DecodeId || a.IsPublic) || user.Role == UserRole.Admin))
            .ThenInclude(i => i.Author)
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync();

        return new Response<WikiCategoryResponse>(
            _mapper.Map<WikiCategoryResponse>(category)
        );
    }

    public async Task<Response<WikiArticleResponse>> UpdateWikiArticleAsync(
        WikiArticleResponse request,
        UserResponse user
    )
    {
        var article = await _context.WikiArticles
            .Where(i => i.Id == request.Id)
            .Where(i => user.Role == UserRole.Admin || i.AuthorId == user.DecodeId)
            .FirstOrDefaultAsync();

        if (article is null)
        {
            return new Response<WikiArticleResponse>(null);
        }

        article.IsPublic = request.IsPublic;
        article.Tags = request.Tags;
        article.Title = request.Title;
        article.Contents = request.Contents;
        article.UpdatedAt = DateTime.UtcNow;

        if (request.Category is not null && request.Category.Id != 0)
        {
            article.CategoryId = request.Category.Id;
        }

        await _context.SaveChangesAsync();

        return await GetWikiArticleByIdAsync(request.Id, user);
    }

    public async Task<Response<WikiCategoryResponse>> UpdateWikiCategoryAsync(
        WikiCategoryResponse request
    )
    {
        var category = await _context.WikiCategories
            .Where(i => i.Id == request.Id)
            .FirstOrDefaultAsync();

        if (category is null)
        {
            return new Response<WikiCategoryResponse>(null);
        }

        UpdateMapper.MapValues(request, category);

        await _context.SaveChangesAsync();

        return new Response<WikiCategoryResponse>(
            _mapper.Map<WikiCategoryResponse>(category)
        );
    }

    public async Task<Response<WikiArticleResponse>> DeleteWikiArticleAsync(
        int id,
        UserResponse user
    )
    {
        var article = await _context.WikiArticles
            .Where(i => i.Id == id)
            .Where(i => user.Role == UserRole.Admin || i.AuthorId == user.DecodeId)
            .FirstOrDefaultAsync();

        if (article is null)
        {
            return new Response<WikiArticleResponse>(null);
        }

        _context.WikiArticles.Remove(article);
        await _context.SaveChangesAsync();

        return new Response<WikiArticleResponse>("Operation successful", HttpStatusCode.OK);
    }

    public async Task<Response<WikiCategoryResponse>> DeleteWikiCategoryAsync(int id)
    {
        var category = await _context.WikiCategories
            .FirstOrDefaultAsync(i => i.Id == id);

        if (category is null)
        {
            return new Response<WikiCategoryResponse>(null);
        }

        _context.WikiCategories.Remove(category);
        await _context.SaveChangesAsync();

        return new Response<WikiCategoryResponse>("Operation successful", HttpStatusCode.OK);
    }
}
