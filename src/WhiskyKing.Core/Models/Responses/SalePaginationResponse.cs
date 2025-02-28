namespace WhiskyKing.Core.Models.Responses;

public class SalePaginationResponse
{
    public Guid Id { get; set; }
    public DateTime RegisterDate { get; set; }
    public decimal Amount { get; set; }
    public bool Inactive { get; set; }
}