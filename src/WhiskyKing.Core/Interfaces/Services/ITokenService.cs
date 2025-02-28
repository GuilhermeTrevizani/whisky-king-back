using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Core.Interfaces.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}