using ProjectBlu.Repositories;
using ProjectBlu.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using ProjectBlu.Settings;
using ProjectBlu.Models;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace ProjectBlu.Services;

public class AuthService : IAuthService
{
    private readonly ProjectBluContext _context;
    private readonly JwtSettings _jwtSettings;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public AuthService(
        ProjectBluContext context,
        IConfiguration configuration,
        IUserService userService,
        IMapper mapper
    )
    {
        _context = context;
        _mapper = mapper;
        _userService = userService;
        _jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();
    }

    public async Task<Response<LoginResponse>> GetOrCreateOpenIdUserAsync(User user)
    {
        var databaseUser = await _context.Users
            .Where(u => !u.DeletedAt.HasValue)
            .Where(u => u.Email == user.Email)
            .FirstOrDefaultAsync();

        if (databaseUser != null)
        {
            return new Response<LoginResponse>(
                new LoginResponse
                {
                    Token = GenerateJwtToken(databaseUser),
                    User = _mapper.Map<UserResponse>(user)
                }
            );
        }

        var canSetup = await _userService.CanSetupAsync();
        user.Role = canSetup ? UserRole.Admin : UserRole.User;

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new Response<LoginResponse>(
            new LoginResponse
            {
                Token = GenerateJwtToken(user),
                User = _mapper.Map<UserResponse>(user)
            }
        );
    }

    public async Task<Response<UserResponse>> GetUserAsync(int id)
    {
        var user = await _context.Users
            .Where(u => !u.DeletedAt.HasValue)
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();

        if (user is null)
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        return new Response<UserResponse>(
            _mapper.Map<UserResponse>(user)
        );
    }

    public async Task<Response<LoginResponse>> LoginAsync(LoginRequest request)
    {
        var user = await _context.Users
            .Where(u => !u.DeletedAt.HasValue)
            .Where(u => u.Email == request.Email && u.Password != null)
            .FirstOrDefaultAsync();

        if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        string token = GenerateJwtToken(user);

        return new Response<LoginResponse>(
            new LoginResponse
            {
                Token = token,
                User = _mapper.Map<UserResponse>(user)
            }
        );
    }

    private string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtSettings.Key)
        );

        var credentials = new SigningCredentials(
            securityKey,
            SecurityAlgorithms.HmacSha256
        );

        var claims = new List<Claim>
        {
            new Claim("id", user.Id.ToString()),
            new Claim("firstName", user.FirstName),
            new Claim("lastName", user.LastName),
            new Claim("email", user.Email),
            new Claim("role", user.Role.ToString()),
            new Claim("createdAt", user.CreatedAt.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(120),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
