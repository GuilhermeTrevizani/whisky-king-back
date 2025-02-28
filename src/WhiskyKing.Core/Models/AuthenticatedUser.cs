using Microsoft.AspNetCore.Http;
using WhiskyKing.Core.Interfaces;
using WhiskyKing.Domain.Enums;

namespace WhiskyKing.Core.Models;

public class AuthenticatedUser(IHttpContextAccessor httpContextAccessor) : IAuthenticatedUser
{
    public Guid? Id
    {
        get
        {
            var id = httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == nameof(Id))?.Value;
            return string.IsNullOrWhiteSpace(id) ? null : new Guid(id);
        }
    }

    public IEnumerable<Permission> Permissions { get; set; } = [];
}