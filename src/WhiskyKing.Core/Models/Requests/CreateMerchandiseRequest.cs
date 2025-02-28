namespace WhiskyKing.Core.Models.Requests;

public class CreateMerchandiseRequest
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
}