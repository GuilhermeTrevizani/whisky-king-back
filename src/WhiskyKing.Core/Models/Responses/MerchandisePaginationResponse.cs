namespace WhiskyKing.Core.Models.Responses;

public class MerchandisePaginationResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool Inactive { get; set; }
    public DateTime RegisterDate { get; set; }
    public Guid CategoryId { get; set; }
}