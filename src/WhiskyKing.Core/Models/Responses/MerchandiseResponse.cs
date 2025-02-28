namespace WhiskyKing.Core.Models.Responses;

public class MerchandiseResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
    public bool Inactive { get; set; }
}