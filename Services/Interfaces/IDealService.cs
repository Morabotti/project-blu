namespace ProjectBlu.Services.Interfaces;

public interface IDealService
{
    Task<Response<DealResponse>> CreateAsync(CreateDealRequest request);
    Task<Response<DealResponse>> GetByIdAsync(int id);
    Task<Response<PaginationResponse<DealResponse>>> GetManyAsync(DealQuery query, PaginationQuery pagination);
    Task<Response<DealResponse>> UpdateAsync(DealResponse request, UserResponse user);
    Task<Response<DealResponse>> DeleteAsync(int id, UserResponse user);
}
