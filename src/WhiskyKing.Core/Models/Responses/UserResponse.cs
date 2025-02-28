namespace WhiskyKing.Core.Models.Responses;

public class UserResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public IEnumerable<Guid> AccessGroups { get; set; } = [];
    public bool Inactive { get; set; }
}