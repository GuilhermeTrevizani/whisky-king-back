using Microsoft.AspNetCore.Mvc;
using WhiskyKing.Core.Models.Requests;
using WhiskyKing.Core.Models.Responses;

namespace WhiskyKing.Core.Interfaces.Services;

public interface IUserService
{
    Task<LoginResponse> Login([FromBody] LoginRequest request);
    Task ChangePassword([FromBody] ChangeUserPasswordRequest request);
    Task<Guid> Create([FromBody] CreateUserRequest request);
    Task Update([FromBody] UpdateUserRequest request);
    Task<UserResponse> GetById(Guid id);
    Task<PaginationResponse<UserPaginationResponse>> GetByPagination([FromQuery] PaginationRequest request);
}