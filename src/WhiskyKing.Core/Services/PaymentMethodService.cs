using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WhiskyKing.Core.Extensions;
using WhiskyKing.Core.Interfaces;
using WhiskyKing.Core.Interfaces.Services;
using WhiskyKing.Core.Models.Requests;
using WhiskyKing.Core.Models.Responses;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Core.Services;

public class PaymentMethodService(IUnitOfWork uow, IValidator<PaymentMethod> validator) : IPaymentMethodService
{
    public async Task<Guid> Create([FromBody] CreatePaymentMethodRequest request)
    {
        var paymentMethod = new PaymentMethod(request.Name);

        await paymentMethod.ValidateAndThrowAsync(validator);

        await uow.PaymentMethodRepository.AddAsync(paymentMethod);
        await uow.Commit();

        return paymentMethod.Id;
    }

    public Task<List<ChartResponse>> Get10MostUsed(Guid? shiftId)
    {
        return uow.SalePaymentMethodRepository.Get10PaymentMethodsMostUsed(shiftId);
    }

    public async Task<PaymentMethodResponse> GetById(Guid id)
    {
        var paymentMethod = await uow.PaymentMethodRepository.GetById(id) ?? throw new ArgumentException(Globalization.Resources.RecordNotFound);

        return new()
        {
            Id = paymentMethod.Id,
            Name = paymentMethod.Name,
            Inactive = paymentMethod.DeletedDate.HasValue
        };
    }

    public async Task<PaginationResponse<PaymentMethodPaginationResponse>> GetByPagination([FromQuery] PaginationRequest request)
    {
        var paginationRepositoryRequest = new PaginationRepositoryRequest<PaymentMethod>(request,
                new()
                {
                    { "id", x => x.Id },
                    { "name", x => x.Name },
                    { "registerdate", x => x.RegisterDate },
                },
                x => string.IsNullOrWhiteSpace(request.Search)
                    || x.Name.Contains(request.Search)
                    || x.RegisterDate.ToString().Contains(request.Search));

        var paginationRepositoryResponse = await uow.PaymentMethodRepository.GetPagination(paginationRepositoryRequest);

        var paginationResponse = new PaginationResponse<PaymentMethodPaginationResponse>
        {
            Data = paginationRepositoryResponse.Data.Select(x => new PaymentMethodPaginationResponse
            {
                Id = x.Id,
                Name = x.Name,
                Inactive = x.DeletedDate.HasValue,
                RegisterDate = x.RegisterDate
            }),
            RecordsTotal = paginationRepositoryResponse.TotalCount,
        };

        return paginationResponse;
    }

    public async Task Update([FromBody] UpdatePaymentMethodRequest request)
    {
        var paymentMethod = await uow.PaymentMethodRepository.GetById(request.Id) ?? throw new ArgumentException(Globalization.Resources.RecordNotFound);

        paymentMethod.Update(request.Name, request.Inactive ? DateTime.Now : null);

        await paymentMethod.ValidateAndThrowAsync(validator);

        uow.PaymentMethodRepository.Update(paymentMethod);

        await uow.Commit();
    }
}