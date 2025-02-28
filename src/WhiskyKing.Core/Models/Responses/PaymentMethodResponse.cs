namespace WhiskyKing.Core.Models.Responses;

public class PaymentMethodResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool Inactive { get; set; }
}