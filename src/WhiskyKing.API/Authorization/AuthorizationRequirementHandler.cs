using Microsoft.AspNetCore.Authorization;
using WhiskyKing.Core.Interfaces;
using WhiskyKing.Core.Models;
using WhiskyKing.Domain.Enums;

namespace WhiskyKing.API.Authorization;

public class AuthorizationRequirementHandler(
    IUnitOfWork uow,
    IAuthenticatedUser authenticatedUser
        ) : AuthorizationHandler<AuthorizationRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizationRequirement requirement)
    {
        if (!authenticatedUser.Id.HasValue)
            return;

        var user = await uow.UserRepository.GetActiveById(authenticatedUser.Id.Value);
        if (user == null)
            return;

        var isAdmin = user.UsersAccessGroups!.Any(x => x.AccessGroupId == new Guid(Constants.ID_ACCESS_GROUP_ADMIN));

        authenticatedUser.Permissions = isAdmin ?
            Enum.GetValues<Permission>()
            :
            user.UsersAccessGroups!.SelectMany(x => x.AccessGroup!.AccessGroupsPermissions!.Select(y => y.Permission));

        if (requirement.Permissions.Length == 0)
        {
            context.Succeed(requirement);
            return;
        }

        foreach (var permission in requirement.Permissions)
            if (authenticatedUser.Permissions.Contains(permission))
                context.Succeed(requirement);
    }
}