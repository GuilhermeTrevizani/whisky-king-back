using Microsoft.AspNetCore.Mvc;
using WhiskyKing.Core.Models.Requests;
using WhiskyKing.Core.Models.Responses;

namespace WhiskyKing.Core.Interfaces.Services;

public interface IPaymentMethodService
{
    Task<PaymentMethodResponse> GetById(Guid id);
    Task<PaginationResponse<PaymentMethodPaginationResponse>> GetByPagination([FromQuery] PaginationRequest request);
    Task<Guid> Create([FromBody] CreatePaymentMethodRequest request);
    Task Update([FromBody] UpdatePaymentMethodRequest request);
    Task<List<ChartResponse>> Get10MostUsed(Guid? shiftId);
}