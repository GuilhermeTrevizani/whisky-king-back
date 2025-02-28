using Microsoft.AspNetCore.Authorization;
using WhiskyKing.Domain.Enums;

namespace WhiskyKing.API.Authorization;

public class AuthorizationRequirement(Permission[] permissions) : IAuthorizationRequirement
{
    public Permission[] Permissions { get; } = permissions;
}