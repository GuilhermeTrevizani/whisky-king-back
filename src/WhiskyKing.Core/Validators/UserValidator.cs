using FluentValidation;
using WhiskyKing.Core.Interfaces;
using WhiskyKing.Core.Models;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Core.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator(IUnitOfWork uow)
    {
        RuleFor(x => x.Id)
            .Must(x => x != new Guid(Constants.ID_USER_ADMIN))
            .WithMessage(Globalization.Resources.UserAdminCantBeModified);

        RuleFor(x => x.Name)
            .Length(1, 100)
            .WithMessage(Globalization.Resources.NameMustBeBetween1And100Characters);

        RuleFor(x => x.Login)
            .Length(1, 100)
            .WithMessage(Globalization.Resources.LoginMustBeBetween1And100Characters);

        RuleFor(x => x.Login)
            .MustAsync(async (user, login, cancellation) =>
            {
                var userLogin = await uow.UserRepository.GetByLogin(login);
                return userLogin is null || userLogin.Id == user.Id;
            })
            .WithMessage(Globalization.Resources.LoginAlreadyInUse);
    }
}