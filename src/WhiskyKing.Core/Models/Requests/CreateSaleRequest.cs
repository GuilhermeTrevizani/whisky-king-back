namespace WhiskyKing.Core.Models.Requests;

public class CreateSaleRequest
{
    public IEnumerable<Merchandise> Merchandises { get; set; } = [];
    public IEnumerable<PaymentMethod> PaymentMethods { get; set; } = [];

    public class Merchandise
    {
        public Guid MerchandiseId { get; set; }
        public decimal Quantity { get; set; }
        public string? Detail { get; set; }
        public decimal Discount { get; set; }
    }

    public class PaymentMethod
    {
        public Guid PaymentMethodId { get; set; }
        public decimal Value { get; set; }
    }
}