using ProjectBlu.Models;
using ProjectBlu.Repositories;
using ProjectBlu.Services.Interfaces;

namespace ProjectBlu.Services;

public class UserService : IUserService
{
    private readonly ProjectBluContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<IUserService> _logger;

    private readonly IMailService _mailService;

    public UserService(
        ProjectBluContext context,
        IMapper mapper,
        IMailService mailService,
        ILogger<IUserService> logger
    )
    {
        _context = context;
        _mapper = mapper;
        _mailService = mailService;
        _logger = logger;
    }

    public async Task<UserToken> CreateRecoveryTokenAsync(UserResponse user)
    {
        var token = new UserToken
        {
            ExpiresAt = DateTime.UtcNow.AddDays(1),
            Type = TokenType.ResetPassword,
            Token = $"{Guid.NewGuid()}-{Guid.NewGuid()}",
            UserId = user.Id
        };

        _context.UserTokens.Add(token);
        _mailService.SendResetPasswordEmail(user.Email, token.Token);

        await _context.SaveChangesAsync();

        return token;
    }

    public async Task ClearTokensAsync()
    {
        if (!_context.Database.CanConnect())
        {
            _logger.LogWarning("Trying to clear database tokens, but can't.");
            return;
        }

        DateTime now = DateTime.UtcNow;

        var tokens = await _context.UserTokens
            .Where(token => token.ExpiresAt < now)
            .ToListAsync();
            
        _context.UserTokens.RemoveRange(tokens);
        await _context.SaveChangesAsync();
    }
}
