using FluentValidation;
using WhiskyKing.Core.Extensions;
using WhiskyKing.Core.Interfaces;
using WhiskyKing.Core.Interfaces.Services;
using WhiskyKing.Core.Models.Requests;
using WhiskyKing.Core.Models.Responses;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Core.Services;

public class MerchandiseService(IUnitOfWork uow, IValidator<Merchandise> validator) : IMerchandiseService
{
    public async Task<Guid> Create(CreateMerchandiseRequest request)
    {
        var merchandise = new Merchandise();
        merchandise.Create(request.Name, request.CategoryId, request.Price);

        await merchandise.ValidateAndThrowAsync(validator);

        await uow.MerchandiseRepository.AddAsync(merchandise);
        await uow.Commit();

        return merchandise.Id;
    }

    public Task<List<ChartResponse>> Get10BestSellers(Guid? shiftId)
    {
        return uow.SaleMerchandiseRepository.Get10MerchandisesBestSellers(shiftId);
    }

    public async Task<MerchandiseResponse> GetById(Guid id)
    {
        var merchandise = await uow.MerchandiseRepository.GetById(id) ?? throw new ArgumentException(Globalization.Resources.RecordNotFound);

        return new()
        {
            Id = merchandise.Id,
            Name = merchandise.Name,
            CategoryId = merchandise.CategoryId,
            Price = merchandise.Price,
            Inactive = merchandise.DeletedDate.HasValue
        };
    }

    public async Task<PaginationResponse<MerchandisePaginationResponse>> GetByPagination(PaginationRequest request)
    {
        var paginationRepositoryRequest = new PaginationRepositoryRequest<Merchandise>(request,
                new()
                {
                    { "id", x => x.Id },
                    { "name", x => x.Name },
                    { "price", x => x.Price },
                    { "registerdate", x => x.RegisterDate },
                },
                x => string.IsNullOrWhiteSpace(request.Search)
                    || x.Name.Contains(request.Search)
                    || x.RegisterDate.ToString().Contains(request.Search));

        var paginationRepositoryResponse = await uow.MerchandiseRepository.GetPagination(paginationRepositoryRequest);

        var paginationResponse = new PaginationResponse<MerchandisePaginationResponse>
        {
            Data = paginationRepositoryResponse.Data.Select(x => new MerchandisePaginationResponse
            {
                Id = x.Id,
                Name = x.Name,
                CategoryId = x.CategoryId,
                Price = x.Price,
                Inactive = x.DeletedDate.HasValue,
                RegisterDate = x.RegisterDate
            }),
            RecordsTotal = paginationRepositoryResponse.TotalCount,
        };

        return paginationResponse;
    }

    public async Task Update(UpdateMerchandiseRequest request)
    {
        var merchandise = await uow.MerchandiseRepository.GetById(request.Id) ?? throw new ArgumentException(Globalization.Resources.RecordNotFound);

        merchandise.Update(request.Name, request.CategoryId, request.Price, request.Inactive ? DateTime.Now : null);

        await merchandise.ValidateAndThrowAsync(validator);

        uow.MerchandiseRepository.Update(merchandise);

        await uow.Commit();
    }
}