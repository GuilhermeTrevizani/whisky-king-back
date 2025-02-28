using WhiskyKing.Core.Models.Responses;

namespace WhiskyKing.Core.Interfaces.Services;

public interface IPermissionService
{
    IEnumerable<PermissionResponse> GetAll();
}