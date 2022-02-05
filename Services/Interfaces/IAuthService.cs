using ProjectBlu.Models;

namespace ProjectBlu.Services.Interfaces;

public interface IAuthService
{
    Task<Response<LoginResponse>> LoginAsync(LoginRequest request);
    Task<Response<LoginResponse>> GetOrCreateOpenIdUserAsync(User user);
    Task<Response<UserResponse>> GetUserAsync(int id);
}
