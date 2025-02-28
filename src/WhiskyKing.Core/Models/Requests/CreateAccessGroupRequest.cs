using WhiskyKing.Domain.Enums;

namespace WhiskyKing.Core.Models.Requests;

public class CreateAccessGroupRequest
{
    public string Name { get; set; } = string.Empty;
    public IEnumerable<Permission> Permissions { get; set; } = [];
}