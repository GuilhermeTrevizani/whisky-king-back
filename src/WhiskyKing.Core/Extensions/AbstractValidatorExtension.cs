using FluentValidation;

namespace WhiskyKing.Core.Extensions;

public static class AbstractValidatorExtension
{
    public static async Task ValidateAndThrowAsync<TEntity>(this TEntity entity, IValidator<TEntity> validator)
    {
        var validationResult = await validator.ValidateAsync(entity);
        if (validationResult.Errors.Count == 0)
            return;

        throw new AggregateException(validationResult.Errors.Select(x => new ArgumentException(x.ErrorMessage)));
    }
}