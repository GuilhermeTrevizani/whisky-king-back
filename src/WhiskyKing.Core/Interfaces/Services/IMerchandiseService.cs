using WhiskyKing.Core.Models.Requests;
using WhiskyKing.Core.Models.Responses;

namespace WhiskyKing.Core.Interfaces.Services;

public interface IMerchandiseService
{
    Task<MerchandiseResponse> GetById(Guid id);
    Task<PaginationResponse<MerchandisePaginationResponse>> GetByPagination(PaginationRequest request);
    Task<Guid> Create(CreateMerchandiseRequest request);
    Task Update(UpdateMerchandiseRequest request);
    Task<List<ChartResponse>> Get10BestSellers(Guid? shiftId);
}