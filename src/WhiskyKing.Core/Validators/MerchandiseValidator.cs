using FluentValidation;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Core.Validators;

public class MerchandiseValidator : AbstractValidator<Merchandise>
{
    public MerchandiseValidator()
    {
        RuleFor(x => x.Name)
            .Length(1, 50)
            .WithMessage(Globalization.Resources.NameMustBeBetween1And50Characters);

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage(Globalization.Resources.InvalidPrice);
    }
}