using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhiskyKing.Core.Interfaces.Services;
using WhiskyKing.Core.Models.Requests;
using WhiskyKing.Core.Models.Responses;
using WhiskyKing.Domain.Enums;

namespace WhiskyKing.API.Controllers;

[Route("users")]
public class UsersController(IUserService userService) : BaseController
{
    [HttpPost("login"), AllowAnonymous]
    public Task<LoginResponse> Login([FromBody] LoginRequest request)
    {
        return userService.Login(request);
    }

    [HttpPut("change-password")]
    public Task ChangePassword([FromBody] ChangeUserPasswordRequest request)
    {
        return userService.ChangePassword(request);
    }

    [HttpPost, Authorize(Policy = nameof(Permission.ManageUsers))]
    public Task<Guid> Create([FromBody] CreateUserRequest request)
    {
        return userService.Create(request);
    }

    [HttpPut, Authorize(Policy = nameof(Permission.ManageUsers))]
    public Task Update([FromBody] UpdateUserRequest request)
    {
        return userService.Update(request);
    }

    [HttpGet("{id}"), Authorize(Policy = nameof(Permission.ViewUsers))]
    public Task<UserResponse> GetById(Guid id)
    {
        return userService.GetById(id);
    }

    [HttpGet("pagination")]
    public Task<PaginationResponse<UserPaginationResponse>> GetByPagination([FromQuery] PaginationRequest request)
    {
        return userService.GetByPagination(request);
    }
}