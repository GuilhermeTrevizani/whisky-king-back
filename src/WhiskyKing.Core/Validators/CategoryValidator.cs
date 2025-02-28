using FluentValidation;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Core.Validators;

public class CategoryValidator : AbstractValidator<Category>
{
    public CategoryValidator()
    {
        RuleFor(x => x.Name)
            .Length(1, 25)
            .WithMessage(Globalization.Resources.NameMustBeBetween1And25Characters);
    }
}