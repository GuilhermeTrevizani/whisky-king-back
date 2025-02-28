using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhiskyKing.Core.Interfaces.Services;
using WhiskyKing.Core.Models.Responses;
using WhiskyKing.Domain.Enums;

namespace WhiskyKing.API.Controllers;

[Route("shifts"), Authorize(Policy = nameof(Permission.ViewSales))]
public class ShiftsController(IShiftService shiftService) : BaseController
{
    [HttpGet("current")]
    public Task<ShiftResponse?> GetCurrent()
    {
        return shiftService.GetCurrent();
    }

    [HttpPost("start"), Authorize(Policy = nameof(Permission.ManageSales))]
    public Task<ShiftResponse> Start()
    {
        return shiftService.Start();
    }

    [HttpPut("end"), Authorize(Policy = nameof(Permission.ManageSales))]
    public Task End()
    {
        return shiftService.End();
    }

    [HttpGet]
    public Task<IEnumerable<ShiftResponse>> GetAll()
    {
        return shiftService.GetAll();
    }
}