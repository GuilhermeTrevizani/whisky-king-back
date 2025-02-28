namespace WhiskyKing.Core.Models.Requests;

public class UpdateUserRequest : CreateUserRequest
{
    public Guid Id { get; set; }
    public bool Inactive { get; set; }
}