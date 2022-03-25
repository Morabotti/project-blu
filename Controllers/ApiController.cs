using HashidsNet;
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

        var decoded = GetHashIds().Decode(idClaim);

        if (decoded is null || decoded.Length == 0)
        {
            throw new ArgumentNullException("Id decode failed.");
        }

        return new UserResponse
        {
            Id = idClaim,
            DecodeId = decoded[0],
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

    private IHashids GetHashIds()
    {
        return (IHashids)HttpContext.RequestServices.GetService(typeof(IHashids));
    }
}