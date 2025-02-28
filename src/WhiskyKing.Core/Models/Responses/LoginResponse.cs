using WhiskyKing.Domain.Enums;

namespace WhiskyKing.Core.Models.Responses;

public class LoginResponse
{
    public string Name { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public IEnumerable<Permission> Permissions { get; set; } = [];
}