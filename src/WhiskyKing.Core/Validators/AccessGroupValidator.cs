using FluentValidation;
using WhiskyKing.Core.Models;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Core.Validators;

public class AccessGroupValidator : AbstractValidator<AccessGroup>
{
    public AccessGroupValidator()
    {
        RuleFor(x => x.Id)
            .Must(x => x != new Guid(Constants.ID_ACCESS_GROUP_ADMIN))
            .WithMessage(Globalization.Resources.AccessGroupAdminCantBeModified);

        RuleFor(x => x.Name)
            .Length(1, 25)
            .WithMessage(Globalization.Resources.NameMustBeBetween1And25Characters);
    }
}