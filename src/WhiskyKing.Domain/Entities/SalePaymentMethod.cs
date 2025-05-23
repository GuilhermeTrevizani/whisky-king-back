﻿namespace WhiskyKing.Domain.Entities;

public class SalePaymentMethod : BaseEntityMin
{
    public Guid SaleId { get; private set; }
    public Guid PaymentMethodId { get; private set; }
    public decimal Value { get; private set; }

    public Sale? Sale { get; private set; }
    public PaymentMethod? PaymentMethod { get; private set; }

    public void Create(Guid paymentMethodId, decimal value)
    {
        PaymentMethodId = paymentMethodId;
        Value = value;
    }
}