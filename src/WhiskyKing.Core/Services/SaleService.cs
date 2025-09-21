using FluentValidation;
using WhiskyKing.Core.Extensions;
using WhiskyKing.Core.Interfaces;
using WhiskyKing.Core.Interfaces.Services;
using WhiskyKing.Core.Models.Requests;
using WhiskyKing.Core.Models.Responses;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Core.Services;

public class SaleService(IUnitOfWork uow, IValidator<Sale> validator) : ISaleService
{
    public async Task<Guid> Create(CreateSaleRequest request)
    {
        var currentShift = await uow.ShiftRepository.GetCurrent()
            ?? throw new ArgumentException(Globalization.Resources.ShiftIsClosed);

        var salesMerchandises = new List<SaleMerchandise>();
        foreach (var merchandise in request.Merchandises)
        {
            var saleMerchandise = new SaleMerchandise(merchandise.MerchandiseId, merchandise.Quantity,
                 (await uow.MerchandiseRepository.GetById(merchandise.MerchandiseId))?.Price ?? 0,
                merchandise.Detail, merchandise.Discount);
            salesMerchandises.Add(saleMerchandise);
        }

        var salesPaymentMethods = new List<SalePaymentMethod>();
        foreach (var paymentMethod in request.PaymentMethods)
        {
            var salePaymentMethod = new SalePaymentMethod(paymentMethod.PaymentMethodId, paymentMethod.Value);
            salesPaymentMethods.Add(salePaymentMethod);
        }

        var sale = new Sale(currentShift.Id, salesMerchandises, salesPaymentMethods);

        await sale.ValidateAndThrowAsync(validator);

        await uow.SaleRepository.AddAsync(sale);
        await uow.Commit();

        return sale.Id;
    }

    public async Task<PaginationResponse<SalePaginationResponse>> GetByPagination(PaginationRequest request)
    {
        var paginationRepositoryRequest = new PaginationRepositoryRequest<Sale>(request,
                new()
                {
                    { "id", x => x.Id },
                    { "registerdate", x => x.RegisterDate },
                },
                x => string.IsNullOrWhiteSpace(request.Search)
                    || x.RegisterDate.ToString().Contains(request.Search),
                [nameof(Sale.SalesPaymentMethods)]);

        var paginationRepositoryResponse = await uow.SaleRepository.GetPagination(paginationRepositoryRequest);

        var paginationResponse = new PaginationResponse<SalePaginationResponse>
        {
            Data = paginationRepositoryResponse.Data.Select(x => new SalePaginationResponse
            {
                Id = x.Id,
                RegisterDate = x.RegisterDate,
                Amount = x.SalesPaymentMethods!.Sum(x => x.Value),
                Inactive = x.DeletedDate.HasValue
            }),
            RecordsTotal = paginationRepositoryResponse.TotalCount,
        };

        return paginationResponse;
    }

    public async Task<SaleInvoiceResponse> GetInvoiceById(Guid id)
    {
        var sale = await uow.SaleRepository.GetById(id) ?? throw new ArgumentException(Globalization.Resources.RecordNotFound);

        return new()
        {
            Id = sale.Id,
            RegisterDate = sale.RegisterDate,
            Merchandises = sale.SalesMerchandises!.Select(x => new SaleInvoiceResponse.Merchandise
            {
                Name = x.Merchandise!.Name,
                Quantity = x.Quantity,
                Price = x.Price,
                Detail = x.Detail,
                Discount = x.Discount,
            }),
            PaymentMethods = sale.SalesPaymentMethods!.Select(x => new SaleInvoiceResponse.PaymentMethod
            {
                Name = x.PaymentMethod!.Name,
                Value = x.Value
            }),
        };
    }
}