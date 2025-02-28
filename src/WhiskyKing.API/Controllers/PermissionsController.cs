using Microsoft.AspNetCore.Mvc;
using WhiskyKing.Core.Interfaces.Services;
using WhiskyKing.Core.Models.Responses;

namespace WhiskyKing.API.Controllers;

[Route("permissions")]
public class PermissionsController(IPermissionService permissionService) : BaseController
{
    [HttpGet]
    public IEnumerable<PermissionResponse> GetAll()
    {
        return permissionService.GetAll();
    }
}