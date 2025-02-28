using WhiskyKing.Core.Models.Requests;
using WhiskyKing.Core.Models.Responses;

namespace WhiskyKing.Core.Interfaces.Services;

public interface ICategoryService
{
    Task<CategoryResponse> GetById(Guid id);
    Task<PaginationResponse<CategoryPaginationResponse>> GetByPagination(PaginationRequest request);
    Task<Guid> Create(CreateCategoryRequest request);
    Task Update(UpdateCategoryRequest request);
    Task<List<ChartResponse>> Get10BestSellers(Guid? shiftId);
}