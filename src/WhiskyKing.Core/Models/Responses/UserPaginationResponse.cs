namespace WhiskyKing.Core.Models.Responses;

public class UserPaginationResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public bool Inactive { get; set; }
    public DateTime RegisterDate { get; set; }
}