namespace ProjectBlu.Services.Interfaces;

public interface IAuthService
{
    Task<Response<LoginResponse>> LoginAsync(LoginRequest request);
    Task<Response<UserResponse>> GetUserAsync(int id);
}
