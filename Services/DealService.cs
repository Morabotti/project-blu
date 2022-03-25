using ProjectBlu.Models;
using ProjectBlu.Repositories;
using ProjectBlu.Services.Interfaces;
using ProjectBlu.Utils;

namespace ProjectBlu.Services;

public class DealService : IDealService
{
    private readonly ProjectBluContext _context;
    private readonly IMapper _mapper;

    public DealService(ProjectBluContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<DealResponse>> CreateAsync(
        CreateDealRequest request
    )
    {
        var deal = _mapper.Map<Deal>(request);

        _context.Deals.Add(deal);
        await _context.SaveChangesAsync();

        return new Response<DealResponse>(
            _mapper.Map<DealResponse>(deal)
        );
    }

    public async Task<Response<PaginationResponse<DealResponse>>> GetManyAsync(
        DealQuery query,
        PaginationQuery pagination
    )
    {
        // TODO: Add query filters
        var deals = await _context.Deals
            .Include(i => i.Customer)
            .Include(i => i.Responsible)
            .OrderBy(i => i.Id)
            .Skip(pagination.Offset.GetValueOrDefault(0))
            .Take(pagination.Limit.GetValueOrDefault(20))
            .AsNoTracking()
            .ToListAsync();

        int count = await _context.Deals.CountAsync();

        return new Response<PaginationResponse<DealResponse>>(
            new PaginationResponse<DealResponse>(
                _mapper.Map<List<DealResponse>>(deals),
                count
            )
        );
    }

    public async Task<Response<DealResponse>> GetByIdAsync(int id)
    {
        var group = await _context.Deals
            .Include(i => i.Customer)
            .Include(i => i.Responsible)
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id);

        return new Response<DealResponse>(
            _mapper.Map<DealResponse>(group)
        );
    }

    public async Task<Response<DealResponse>> UpdateAsync(
        DealResponse request,
        UserResponse user
    )
    {
        var deal = await _context.Deals
            .Where(i => user.Role == UserRole.Admin || user.DecodeId == i.ResponsibleId)
            .FirstOrDefaultAsync(i => i.Id == request.Id);

        if (deal is null)
        {
            return new Response<DealResponse>(null);
        }

        UpdateMapper.MapValues(request, deal);

        await _context.SaveChangesAsync();

        return new Response<DealResponse>(
            _mapper.Map<DealResponse>(deal)
        );
    }

    public async Task<Response<DealResponse>> DeleteAsync(int id, UserResponse user)
    {
        var deal = await _context.Deals
            .Where(i => user.Role == UserRole.Admin || user.DecodeId == i.ResponsibleId)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (deal is null)
        {
            return new Response<DealResponse>(null);
        }

        _context.Deals.Remove(deal);
        await _context.SaveChangesAsync();

        return new Response<DealResponse>("Operation successful", HttpStatusCode.OK);
    }
}
