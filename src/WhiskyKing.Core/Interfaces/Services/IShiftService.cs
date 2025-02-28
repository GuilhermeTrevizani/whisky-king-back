using WhiskyKing.Core.Models.Responses;

namespace WhiskyKing.Core.Interfaces.Services;

public interface IShiftService
{
    Task<ShiftResponse?> GetCurrent();
    Task<ShiftResponse> Start();
    Task End();
    Task<IEnumerable<ShiftResponse>> GetAll();
}