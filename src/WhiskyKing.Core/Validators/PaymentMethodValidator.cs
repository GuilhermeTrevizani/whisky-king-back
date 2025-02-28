using FluentValidation;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Core.Validators;

public class PaymentMethodValidator : AbstractValidator<PaymentMethod>
{
    public PaymentMethodValidator()
    {
        RuleFor(x => x.Name)
            .Length(1, 25)
            .WithMessage(Globalization.Resources.NameMustBeBetween1And25Characters);
    }
}