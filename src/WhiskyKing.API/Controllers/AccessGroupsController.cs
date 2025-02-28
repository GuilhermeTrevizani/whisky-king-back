using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhiskyKing.Core.Interfaces.Services;
using WhiskyKing.Core.Models.Requests;
using WhiskyKing.Core.Models.Responses;
using WhiskyKing.Domain.Enums;

namespace WhiskyKing.API.Controllers;

[Route("access-groups")]
public class AccessGroupsController(IAccessGroupService accessGroupService) : BaseController
{
    [HttpGet("{id}"), Authorize(Policy = nameof(Permission.ViewAccessGroups))]
    public Task<AccessGroupResponse> GetById(Guid id)
    {
        return accessGroupService.GetById(id);
    }

    [HttpGet("pagination")]
    public Task<PaginationResponse<AccessGroupPaginationResponse>> GetByPagination([FromQuery] PaginationRequest request)
    {
        return accessGroupService.GetByPagination(request);
    }

    [HttpPost, Authorize(Policy = nameof(Permission.ManageAccessGroups))]
    public Task<Guid> Create([FromBody] CreateAccessGroupRequest request)
    {
        return accessGroupService.Create(request);
    }

    [HttpPut, Authorize(Policy = nameof(Permission.ManageAccessGroups))]
    public Task Update([FromBody] UpdateAccessGroupRequest request)
    {
        return accessGroupService.Update(request);
    }
}