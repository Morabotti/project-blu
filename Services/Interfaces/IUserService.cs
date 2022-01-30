using ProjectBlu.Models;

namespace ProjectBlu.Services.Interfaces;

public interface IUserService
{
    Task<UserToken> CreateRecoveryTokenAsync(UserResponse user);
    Task ClearTokensAsync();
}
