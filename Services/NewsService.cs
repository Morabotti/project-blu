using ProjectBlu.Models;
using ProjectBlu.Repositories;
using ProjectBlu.Services.Interfaces;
using ProjectBlu.Utils;

namespace ProjectBlu.Services;

public class NewsService : INewsService
{
    private readonly ProjectBluContext _context;
    private readonly IMapper _mapper;

    public NewsService(ProjectBluContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<NewsResponse>> CreateAsync(
        CreateNewsRequest request,
        UserResponse user
    )
    {
        var news = _mapper.Map<News>(request);
        news.AuthorId = user.DecodeId;

        _context.News.Add(news);
        await _context.SaveChangesAsync();

        var response = _mapper.Map<NewsResponse>(news);
        response.Author = _mapper.Map<AuthorResponse>(user);

        return new Response<NewsResponse>(response);
    }

    public async Task<Response<PaginationResponse<NewsResponse>>> GetManyAsync(
        PaginationQuery query
    )
    {
        var news = await _context.News
            .Include(i => i.Author)
            .OrderByDescending(order => order.CreatedAt)
            .Skip(query.Offset.GetValueOrDefault(0))
            .Take(query.Limit.GetValueOrDefault(20))
            .AsNoTracking()
            .ToListAsync();

        int count = await _context.News.CountAsync();

        return new Response<PaginationResponse<NewsResponse>>(
            new PaginationResponse<NewsResponse>(
                _mapper.Map<List<NewsResponse>>(news),
                count
            )
        );
    }

    public async Task<Response<NewsResponse>> GetByIdAsync(int id)
    {
        var news = await _context.News
            .Where(i => i.Id == id)
            .Include(i => i.Author)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return new Response<NewsResponse>(
            _mapper.Map<NewsResponse>(news)
        );
    }

    public async Task<Response<NewsResponse>> UpdateAsync(
        NewsResponse request,
        UserResponse user
    )
    {
        var news = await _context.News
            .Where(i => i.Id == request.Id)
            .Where(i => user.Role == UserRole.Admin || i.AuthorId == user.DecodeId)
            .FirstOrDefaultAsync();

        if (news is null)
        {
            return new Response<NewsResponse>(null);
        }

        UpdateMapper.MapValues(request, news);

        // TODO: Check if author has changed

        await _context.SaveChangesAsync();

        return new Response<NewsResponse>(_mapper.Map<NewsResponse>(news));
    }

    public async Task<Response<NewsResponse>> DeleteAsync(
        int id,
        UserResponse user
    )
    {
        var news = await _context.News
            .Where(i => i.Id == id)
            .Where(i => user.Role == UserRole.Admin || i.AuthorId == user.DecodeId)
            .FirstOrDefaultAsync();

        if (news is null)
        {
            return new Response<NewsResponse>(null);
        }

        _context.News.Remove(news);
        await _context.SaveChangesAsync();

        return new Response<NewsResponse>("Operation successful", HttpStatusCode.OK);
    }
}
