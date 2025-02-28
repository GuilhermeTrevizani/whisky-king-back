namespace WhiskyKing.Core.Models.Responses;

public class SaleInvoiceResponse
{
    public Guid Id { get; set; }
    public DateTime RegisterDate { get; set; }
    public IEnumerable<Merchandise> Merchandises { get; set; } = [];
    public IEnumerable<PaymentMethod> PaymentMethods { get; set; } = [];
    public decimal Amount => PaymentMethods.Sum(x => x.Value);

    public class Merchandise
    {
        public string Name { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Subtotal => Quantity * Price - Discount;
        public string? Detail { get; set; }
        public decimal Discount { get; set; }
    }

    public class PaymentMethod
    {
        public string Name { get; set; } = string.Empty;
        public decimal Value { get; set; }
    }
}