using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhiskyKing.Core.Interfaces.Services;
using WhiskyKing.Core.Models.Requests;
using WhiskyKing.Core.Models.Responses;
using WhiskyKing.Domain.Enums;

namespace WhiskyKing.API.Controllers;

[Route("sales"), Authorize(Policy = nameof(Permission.ViewSales))]
public class SalesController(ISaleService saleService) : BaseController
{
    [HttpGet("invoice/{id}")]
    public Task<SaleInvoiceResponse> GetInvoiceById(Guid id)
    {
        return saleService.GetInvoiceById(id);
    }

    [HttpGet("pagination")]
    public Task<PaginationResponse<SalePaginationResponse>> GetByPagination([FromQuery] PaginationRequest request)
    {
        return saleService.GetByPagination(request);
    }

    [HttpPost, Authorize(Policy = nameof(Permission.ManageSales))]
    public Task<Guid> Create([FromBody] CreateSaleRequest request)
    {
        return saleService.Create(request);
    }
}