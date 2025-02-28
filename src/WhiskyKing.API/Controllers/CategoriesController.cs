using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhiskyKing.Core.Interfaces.Services;
using WhiskyKing.Core.Models.Requests;
using WhiskyKing.Core.Models.Responses;
using WhiskyKing.Domain.Enums;

namespace WhiskyKing.API.Controllers;

[Route("categories")]
public class CategoriesController(ICategoryService categoryService) : BaseController
{
    [HttpGet("{id}"), Authorize(Policy = nameof(Permission.ViewCategories))]
    public Task<CategoryResponse> GetById(Guid id)
    {
        return categoryService.GetById(id);
    }

    [HttpGet("pagination")]
    public Task<PaginationResponse<CategoryPaginationResponse>> GetByPagination([FromQuery] PaginationRequest request)
    {
        return categoryService.GetByPagination(request);
    }

    [HttpPost, Authorize(Policy = nameof(Permission.ManageCategories))]
    public Task<Guid> Create([FromBody] CreateCategoryRequest request)
    {
        return categoryService.Create(request);
    }

    [HttpPut, Authorize(Policy = nameof(Permission.ManageCategories))]
    public Task Update([FromBody] UpdateCategoryRequest request)
    {
        return categoryService.Update(request);
    }

    [HttpGet("10-best-sellers"), Authorize(Policy = nameof(Permission.ViewSales))]
    public Task<List<ChartResponse>> Get10BestSellers(Guid? shiftId)
    {
        return categoryService.Get10BestSellers(shiftId);
    }
}