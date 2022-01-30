using ProjectBlu.Models;

namespace ProjectBlu.Services.Interfaces;

public interface IUserService
{
    Task<Response<UserResponse>> CreateUserAsync(CreateUserRequest request);
    Task<Response<UserResponse>> CreateFirstUserAsync(CreateUserRequest request);
    Task<UserToken> CreateRecoveryTokenAsync(UserResponse user);
    Task<bool> CanSetupAsync();
    Task ClearTokensAsync();
}
