namespace WhiskyKing.Core.Models.Requests;

public class UpdateAccessGroupRequest : CreateAccessGroupRequest
{
    public Guid Id { get; set; }
    public bool Inactive { get; set; }
}