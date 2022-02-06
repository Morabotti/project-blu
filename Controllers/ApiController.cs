using ProjectBlu.Enums;
using ProjectBlu.Models;
using System.Security.Claims;

namespace ProjectBlu.Controllers;

public class ApiController : ControllerBase
{
    protected ObjectResult HttpResponse<T>(Response<T> response)
    {
        return StatusCode(
            (int)response.StatusCode,
            response.Success ? response.Resource : response.Message
        );
    }

    protected UserResponse GetUserClaim()
    {
        string idClaim = GetClaimValue(ClaimTypes.NameIdentifier);

        if (idClaim is null)
        {
            throw new ArgumentNullException("Id claim not found!");
        }

        return new UserResponse
        {
            Id = int.Parse(idClaim),
            FirstName = GetClaimValue(JwtClaims.GivenName),
            LastName = GetClaimValue(JwtClaims.FamilyName),
            Email = GetClaimValue(JwtClaims.Email),
            Role = GetClaimValue(JwtClaims.Role) == "Admin" ? UserRole.Admin : UserRole.User,
            CreatedAt = DateTime.Parse(GetClaimValue(JwtClaims.CreatedAt))
        };
    }

    private string GetClaimValue(string claimType)
    {
        return User.FindFirst(claimType)?.Value;
    }
}