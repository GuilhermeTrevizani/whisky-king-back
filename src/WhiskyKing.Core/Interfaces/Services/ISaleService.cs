using WhiskyKing.Core.Models.Requests;
using WhiskyKing.Core.Models.Responses;

namespace WhiskyKing.Core.Interfaces.Services;

public interface ISaleService
{
    Task<SaleInvoiceResponse> GetInvoiceById(Guid id);
    Task<PaginationResponse<SalePaginationResponse>> GetByPagination(PaginationRequest request);
    Task<Guid> Create(CreateSaleRequest request);
}