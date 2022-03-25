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
            UserId = user.DecodeId
        };

        _context.UserTokens.Add(token);
        _mailService.SendResetPasswordEmail(user.Email, token.Token);

        await _context.SaveChangesAsync();

        return token;
    }

    public async Task<Response<UserResponse>> CreateUserAsync(CreateUserRequest request)
    {
        var user = _mapper.Map<User>(request);
        user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new Response<UserResponse>(
            _mapper.Map<UserResponse>(user)
        );
    }

    public async Task<Response<UserResponse>> CreateFirstUserAsync(CreateUserRequest request)
    {
        var canSetup = await CanSetupAsync();

        if (!canSetup)
        {
            return new Response<UserResponse>(null);
        }

        var user = _mapper.Map<User>(request);
        user.Role = UserRole.Admin;
        user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new Response<UserResponse>(
            _mapper.Map<UserResponse>(user)
        );
    }

    public async Task<bool> CanSetupAsync()
    {
        var user = await _context.Users.FirstOrDefaultAsync();
        return user == null;
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
