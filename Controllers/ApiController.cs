﻿using Microsoft.AspNetCore.Mvc;
using ProjectBlu.Dto.Authentication;
using ProjectBlu.Models;

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
        string idClaim = GetClaimValue("id");

        if (idClaim is null)
        {
            throw new ArgumentNullException("Id claim not found!");
        }

        return new UserResponse
        {
            Id = int.Parse(idClaim),
            FirstName = GetClaimValue("firstName"),
            LastName = GetClaimValue("lastName"),
            Email = GetClaimValue("email"),
            Role = (UserRole)Enum.Parse(typeof(UserRole), GetClaimValue("role")),
            CreatedAt = DateTime.Parse(GetClaimValue("createdAt"))
        };
    }

    private string GetClaimValue(string claimType)
    {
        return User.FindFirst(claimType)?.Value;
    }
}