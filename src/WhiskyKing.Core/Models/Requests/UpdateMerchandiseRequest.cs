namespace WhiskyKing.Core.Models.Requests;

public class UpdateMerchandiseRequest : CreateMerchandiseRequest
{
    public Guid Id { get; set; }
    public bool Inactive { get; set; }
}