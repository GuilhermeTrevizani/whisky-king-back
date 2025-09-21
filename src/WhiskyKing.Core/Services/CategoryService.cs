using FluentValidation;
using WhiskyKing.Core.Extensions;
using WhiskyKing.Core.Interfaces;
using WhiskyKing.Core.Interfaces.Services;
using WhiskyKing.Core.Models.Requests;
using WhiskyKing.Core.Models.Responses;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Core.Services;

public class CategoryService(IUnitOfWork uow, IValidator<Category> validator) : ICategoryService
{
    public async Task<Guid> Create(CreateCategoryRequest request)
    {
        var categoryDetails = new List<CategoryDetail>();
        foreach (var detail in request.Details)
        {
            var categoryDetail = new CategoryDetail(detail);
            categoryDetails.Add(categoryDetail);
        }

        var category = new Category(request.Name, categoryDetails);

        await category.ValidateAndThrowAsync(validator);

        await uow.CategoryRepository.AddAsync(category);
        await uow.Commit();

        return category.Id;
    }

    public Task<List<ChartResponse>> Get10BestSellers(Guid? shiftId)
    {
        return uow.SaleMerchandiseRepository.Get10CategoriesBestSellers(shiftId);
    }

    public async Task<CategoryResponse> GetById(Guid id)
    {
        var category = await uow.CategoryRepository.GetById(id) ?? throw new ArgumentException(Globalization.Resources.RecordNotFound);

        return new()
        {
            Id = category.Id,
            Name = category.Name,
            Details = category.Details!.Select(x => x.Detail),
            Inactive = category.DeletedDate.HasValue
        };
    }

    public async Task<PaginationResponse<CategoryPaginationResponse>> GetByPagination(PaginationRequest request)
    {
        var paginationRepositoryRequest = new PaginationRepositoryRequest<Category>(request,
                new()
                {
                    { "id", x => x.Id },
                    { "name", x => x.Name },
                    { "registerdate", x => x.RegisterDate },
                },
                x => string.IsNullOrWhiteSpace(request.Search)
                    || x.Name.Contains(request.Search)
                    || x.RegisterDate.ToString().Contains(request.Search),
                [nameof(Category.Details)]);

        var paginationRepositoryResponse = await uow.CategoryRepository.GetPagination(paginationRepositoryRequest);

        var paginationResponse = new PaginationResponse<CategoryPaginationResponse>
        {
            Data = paginationRepositoryResponse.Data.Select(x => new CategoryPaginationResponse
            {
                Id = x.Id,
                Details = x.Details!.Select(y => y.Detail),
                Name = x.Name,
                RegisterDate = x.RegisterDate,
                Inactive = x.DeletedDate.HasValue
            }),
            RecordsTotal = paginationRepositoryResponse.TotalCount,
        };

        return paginationResponse;
    }

    public async Task Update(UpdateCategoryRequest request)
    {
        var category = await uow.CategoryRepository.GetById(request.Id) ?? throw new ArgumentException(Globalization.Resources.RecordNotFound);

        category.Update(request.Name, request.Inactive ? DateTime.Now : null);

        await category.ValidateAndThrowAsync(validator);

        uow.CategoryRepository.Update(category);

        var detailsDelete = category.Details!
            .Where(x => !request.Details.Contains(x.Detail));
        if (detailsDelete.Any())
            uow.CategoryDetailRepository.DeleteRange(detailsDelete);

        var detailsInsert = new List<CategoryDetail>();
        foreach (var detail in request.Details
            .Where(x => !category.Details!.Any(y => y.Detail == x)))
        {
            var categoryDetail = new CategoryDetail(category.Id, detail);
            detailsInsert.Add(categoryDetail);
        }
        if (detailsInsert.Count != 0)
            await uow.CategoryDetailRepository.AddRangeAsync(detailsInsert);

        await uow.Commit();
    }
}