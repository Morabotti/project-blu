using ProjectBlu.Services.Interfaces;

namespace ProjectBlu.Jobs;

public class UserTokenJobRunner : IHostedService, IDisposable
{
    private readonly IServiceScope _scope;
    private readonly IUserService _userService;
    private readonly ILogger _logger;

    private readonly int _timeoutPeriod = 30;
    private Timer _timer;

    public UserTokenJobRunner(
        IServiceScopeFactory scopeFactory,
        ILogger<UserTokenJobRunner> logger
    )
    {
        _scope = scopeFactory.CreateScope();
        _userService = _scope.ServiceProvider.GetService<IUserService>();
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(
            async (state) => await RunJobAsync(),
            null,
            TimeSpan.FromMinutes(0),
            TimeSpan.FromMinutes(_timeoutPeriod)
        );

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
        _scope?.Dispose();
    }

    private async Task RunJobAsync()
    {
        try
        {
            await _userService.ClearTokensAsync();
        }
        catch (Exception)
        {
            _logger.LogError("Failed to clear tokens.");
        }
    }
}
