using FluentValidation;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Core.Validators;

public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        RuleFor(x => x.SalesMerchandises)
            .Must(x => x.All(y => y.Quantity > 0))
            .WithMessage(Globalization.Resources.AllMerchandisesMustHaveQuantityGreaterThanZero);

        RuleFor(x => x.SalesMerchandises)
            .Must(x => x.All(y => y.Discount >= 0))
            .WithMessage(Globalization.Resources.AllMerchandisesMustHaveDiscountEqualsOrGreaterThanZero);

        RuleFor(x => x.SalesMerchandises)
            .Must(x => x.All(y => y.Price > 0))
            .WithMessage(Globalization.Resources.AllMerchandisesMustHavePriceGreaterThanZero);

        RuleFor(x => x.SalesMerchandises)
            .Must(x => x.All(y => y.Discount <= y.Quantity * y.Price))
            .WithMessage(Globalization.Resources.AllMerchandisesMustHaveDiscountAmountEqualsOrLessThanPrice);

        RuleFor(x => x.SalesPaymentMethods)
            .Must(x => x.All(y => y.Value > 0))
            .WithMessage(Globalization.Resources.AllPaymentMethodsMustHaveValueGreaterThanZero);

        RuleFor(x => x)
            .Must(x => x.SalesMerchandises.Sum(y => y.Quantity * y.Price - y.Discount) == x.SalesPaymentMethods.Sum(y => y.Value))
            .WithMessage(Globalization.Resources.PaymentsAmountMustBeEqualsToSaleAmount);
    }
}