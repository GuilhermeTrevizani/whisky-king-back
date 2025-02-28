using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WhiskyKing.Core.Extensions;
using WhiskyKing.Core.Interfaces;
using WhiskyKing.Core.Interfaces.Services;
using WhiskyKing.Core.Models;
using WhiskyKing.Core.Models.Requests;
using WhiskyKing.Core.Models.Responses;
using WhiskyKing.Domain.Entities;
using WhiskyKing.Domain.Enums;

namespace WhiskyKing.Core.Services;

public class UserService(
    IUnitOfWork uow,
    IValidator<User> validator,
    IAuthenticatedUser authenticatedUser,
    ITokenService tokenService,
    IHashService hashService)
        : IUserService
{
    public async Task ChangePassword([FromBody] ChangeUserPasswordRequest request)
    {
        var user = await uow.UserRepository.GetById(authenticatedUser.Id ?? new Guid());
        if (user!.Id == new Guid(Constants.ID_USER_ADMIN))
            throw new ArgumentException(Globalization.Resources.UserAdminCantBeModified);

        if (!hashService.Verify(request.CurrentPassword, user.Password))
            throw new ArgumentException(Globalization.Resources.CurrentPasswordInvalid);

        if (string.IsNullOrWhiteSpace(request.NewPassword)
            || string.IsNullOrWhiteSpace(request.RepeatNewPassword)
            || request.NewPassword != request.RepeatNewPassword)
            throw new ArgumentException(Globalization.Resources.NewPasswordsInvalid);

        user.ChangePassword(hashService.Hash(request.NewPassword));
        uow.UserRepository.Update(user);
        await uow.Commit();
    }

    public async Task<Guid> Create([FromBody] CreateUserRequest request)
    {
        var userAccessGroups = new List<UserAccessGroup>();
        foreach (var accessGroup in request.AccessGroups)
        {
            var userAccessGroup = new UserAccessGroup();
            userAccessGroup.Create(accessGroup);
            userAccessGroups.Add(userAccessGroup);
        }

        var user = new User();
        user.Create(request.Name, request.Login, hashService.Hash("123"), userAccessGroups);

        await user.ValidateAndThrowAsync(validator);

        await uow.UserRepository.AddAsync(user);

        await uow.Commit();

        return user.Id;
    }

    public async Task<UserResponse> GetById(Guid id)
    {
        var user = await uow.UserRepository.GetById(id) ?? throw new ArgumentException(Globalization.Resources.RecordNotFound);

        return new()
        {
            Id = user.Id,
            Name = user.Name,
            Login = user.Login,
            Inactive = user.DeletedDate.HasValue,
            AccessGroups = user.UsersAccessGroups!.Select(x => x.AccessGroupId)
        };
    }

    public async Task<PaginationResponse<UserPaginationResponse>> GetByPagination([FromQuery] PaginationRequest request)
    {
        var paginationRepositoryRequest = new PaginationRepositoryRequest<User>(request,
                new()
                {
                    { "id", x => x.Id },
                    { "login", x => x.Login },
                    { "name", x => x.Name },
                    { "registerdate", x => x.RegisterDate },
                },
                x => string.IsNullOrWhiteSpace(request.Search)
                    || x.Login.Contains(request.Search)
                    || x.Name.Contains(request.Search)
                    || x.RegisterDate.ToString().Contains(request.Search));

        var paginationRepositoryResponse = await uow.UserRepository.GetPagination(paginationRepositoryRequest);

        var paginationResponse = new PaginationResponse<UserPaginationResponse>
        {
            Data = paginationRepositoryResponse.Data.Select(x => new UserPaginationResponse
            {
                Id = x.Id,
                Name = x.Name,
                Login = x.Login,
                Inactive = x.DeletedDate.HasValue,
                RegisterDate = x.RegisterDate
            }),
            RecordsTotal = paginationRepositoryResponse.TotalCount,
        };

        return paginationResponse;
    }

    public async Task<LoginResponse> Login([FromBody] LoginRequest request)
    {
        var user = await uow.UserRepository.GetActiveByLogin(request.Login);
        if (user is null || !hashService.Verify(request.Password, user.Password))
            throw new ArgumentException(Globalization.Resources.UserOrPasswordInvalids);

        return new()
        {
            Name = user.Name,
            Token = tokenService.GenerateToken(user),
            Permissions = user.UsersAccessGroups!.Any(x => x.AccessGroupId == new Guid(Constants.ID_ACCESS_GROUP_ADMIN)) ?
                Enum.GetValues<Permission>()
            :
                user.UsersAccessGroups!.SelectMany(x => x.AccessGroup!.AccessGroupsPermissions!.Select(y => y.Permission)),
        };
    }

    public async Task Update([FromBody] UpdateUserRequest request)
    {
        var user = await uow.UserRepository.GetById(request.Id) ?? throw new ArgumentException(Globalization.Resources.RecordNotFound);

        user.Update(request.Name, request.Login, request.Inactive ? DateTime.Now : null);

        await user.ValidateAndThrowAsync(validator);

        uow.UserRepository.Update(user);

        var userAcessGroupsDelete = user.UsersAccessGroups!
            .Where(x => !request.AccessGroups.Contains(x.AccessGroupId));
        if (userAcessGroupsDelete.Any())
            uow.UserAccessGroupRepository.DeleteRange(userAcessGroupsDelete);

        var userAcessGroupsInsert = new List<UserAccessGroup>();
        foreach (var accessGroup in request.AccessGroups
            .Where(x => !user.UsersAccessGroups!.Any(y => y.AccessGroupId == x)))
        {
            var userAccessGroup = new UserAccessGroup();
            userAccessGroup.Create(user.Id, accessGroup);
            userAcessGroupsInsert.Add(userAccessGroup);
        }
        if (userAcessGroupsInsert.Count != 0)
            await uow.UserAccessGroupRepository.AddRangeAsync(userAcessGroupsInsert);

        await uow.Commit();
    }
}