namespace WhiskyKing.Core.Models.Requests;

public class UpdatePaymentMethodRequest : CreatePaymentMethodRequest
{
    public Guid Id { get; set; }
    public bool Inactive { get; set; }
}