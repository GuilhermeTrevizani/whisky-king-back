using WhiskyKing.Core.Models.Requests;
using WhiskyKing.Core.Models.Responses;

namespace WhiskyKing.Core.Interfaces.Services;

public interface IAccessGroupService
{
    Task<AccessGroupResponse> GetById(Guid id);
    Task<PaginationResponse<AccessGroupPaginationResponse>> GetByPagination(PaginationRequest request);
    Task<Guid> Create(CreateAccessGroupRequest request);
    Task Update(UpdateAccessGroupRequest request);
}