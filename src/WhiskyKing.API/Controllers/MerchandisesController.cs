using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhiskyKing.Core.Interfaces.Services;
using WhiskyKing.Core.Models.Requests;
using WhiskyKing.Core.Models.Responses;
using WhiskyKing.Domain.Enums;

namespace WhiskyKing.API.Controllers;

[Route("merchandises")]
public class MerchandisesController(IMerchandiseService merchandiseService) : BaseController
{
    [HttpGet("{id}"), Authorize(Policy = nameof(Permission.ViewMerchandises))]
    public Task<MerchandiseResponse> GetById(Guid id)
    {
        return merchandiseService.GetById(id);
    }

    [HttpGet("pagination")]
    public Task<PaginationResponse<MerchandisePaginationResponse>> GetByPagination([FromQuery] PaginationRequest request)
    {
        return merchandiseService.GetByPagination(request);
    }

    [HttpPost, Authorize(Policy = nameof(Permission.ManageMerchandises))]
    public Task<Guid> Create([FromBody] CreateMerchandiseRequest request)
    {
        return merchandiseService.Create(request);
    }

    [HttpPut, Authorize(Policy = nameof(Permission.ManageMerchandises))]
    public Task Update([FromBody] UpdateMerchandiseRequest request)
    {
        return merchandiseService.Update(request);
    }

    [HttpGet("10-best-sellers"), Authorize(Policy = nameof(Permission.ViewSales))]
    public Task<List<ChartResponse>> Get10BestSellers(Guid? shiftId)
    {
        return merchandiseService.Get10BestSellers(shiftId);
    }
}