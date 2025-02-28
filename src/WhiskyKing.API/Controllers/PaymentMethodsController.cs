using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhiskyKing.Core.Interfaces.Services;
using WhiskyKing.Core.Models.Requests;
using WhiskyKing.Core.Models.Responses;
using WhiskyKing.Domain.Enums;

namespace WhiskyKing.API.Controllers;

[Route("payment-methods")]
public class PaymentMethodsController(IPaymentMethodService paymentMethodService) : BaseController
{
    [HttpGet("{id}"), Authorize(Policy = nameof(Permission.ViewPaymentMethods))]
    public Task<PaymentMethodResponse> GetById(Guid id)
    {
        return paymentMethodService.GetById(id);
    }

    [HttpGet("pagination")]
    public Task<PaginationResponse<PaymentMethodPaginationResponse>> GetByPagination([FromQuery] PaginationRequest request)
    {
        return paymentMethodService.GetByPagination(request);
    }

    [HttpPost, Authorize(Policy = nameof(Permission.ManagePaymentMethods))]
    public Task<Guid> Create([FromBody] CreatePaymentMethodRequest request)
    {
        return paymentMethodService.Create(request);
    }

    [HttpPut, Authorize(Policy = nameof(Permission.ManagePaymentMethods))]
    public Task Update([FromBody] UpdatePaymentMethodRequest request)
    {
        return paymentMethodService.Update(request);
    }

    [HttpGet("10-most-used"), Authorize(Policy = nameof(Permission.ViewSales))]
    public Task<List<ChartResponse>> Get10MostUsed(Guid? shiftId)
    {
        return paymentMethodService.Get10MostUsed(shiftId);
    }
}