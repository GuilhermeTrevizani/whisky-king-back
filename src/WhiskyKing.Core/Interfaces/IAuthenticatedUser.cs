using WhiskyKing.Domain.Enums;

namespace WhiskyKing.Core.Interfaces;

public interface IAuthenticatedUser
{
    Guid? Id { get; }
    IEnumerable<Permission> Permissions { get; set; }
}