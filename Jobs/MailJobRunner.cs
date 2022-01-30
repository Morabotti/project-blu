using ProjectBlu.Services.Interfaces;

namespace ProjectBlu.Jobs;

public class MailJobRunner : IHostedService, IDisposable
{
    private readonly IServiceScope _scope;
    private readonly IMailService _mailService;
    private readonly ILogger _logger;

    private readonly int _timeoutPeriod = 1;
    private Timer _timer;

    public MailJobRunner(
        IServiceScopeFactory scopeFactory,
        ILogger<MailJobRunner> logger
    )
    {
        _scope = scopeFactory.CreateScope();
        _mailService = _scope.ServiceProvider.GetService<IMailService>();
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
            await _mailService.SendMailsAsync();
        }
        catch (Exception)
        {
            _logger.LogError("Failed to send mails from queue");
        }
    }
}
