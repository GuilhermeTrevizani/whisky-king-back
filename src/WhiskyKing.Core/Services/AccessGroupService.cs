using FluentValidation;
using WhiskyKing.Core.Extensions;
using WhiskyKing.Core.Interfaces;
using WhiskyKing.Core.Interfaces.Services;
using WhiskyKing.Core.Models.Requests;
using WhiskyKing.Core.Models.Responses;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Core.Services;

public class AccessGroupService(IUnitOfWork uow, IValidator<AccessGroup> validator) : IAccessGroupService
{
    public async Task<Guid> Create(CreateAccessGroupRequest request)
    {
        var accessGroupPermissions = new List<AccessGroupPermission>();
        foreach (var permission in request.Permissions)
        {
            var accessGroupPermission = new AccessGroupPermission(permission);
            accessGroupPermissions.Add(accessGroupPermission);
        }

        var accessGroup = new AccessGroup(request.Name, accessGroupPermissions);

        await accessGroup.ValidateAndThrowAsync(validator);

        await uow.AccessGroupRepository.AddAsync(accessGroup);
        await uow.Commit();

        return accessGroup.Id;
    }

    public async Task<AccessGroupResponse> GetById(Guid id)
    {
        var accessGroup = await uow.AccessGroupRepository.GetById(id) ?? throw new ArgumentException(Globalization.Resources.RecordNotFound);

        return new()
        {
            Id = accessGroup.Id,
            Name = accessGroup.Name,
            Permissions = [.. accessGroup.AccessGroupsPermissions!.Select(x => x.Permission)],
            Inactive = accessGroup.DeletedDate.HasValue
        };
    }

    public async Task<PaginationResponse<AccessGroupPaginationResponse>> GetByPagination(PaginationRequest request)
    {
        var paginationRepositoryRequest = new PaginationRepositoryRequest<AccessGroup>(request,
                new()
                {
                    { "id", x => x.Id },
                    { "name", x => x.Name },
                    { "registerdate", x => x.RegisterDate },
                },
                x => string.IsNullOrWhiteSpace(request.Search) || x.Name.Contains(request.Search));

        var paginationRepositoryResponse = await uow.AccessGroupRepository.GetPagination(paginationRepositoryRequest);

        var paginationResponse = new PaginationResponse<AccessGroupPaginationResponse>
        {
            Data = paginationRepositoryResponse.Data.Select(x => new AccessGroupPaginationResponse
            {
                Id = x.Id,
                Name = x.Name,
                RegisterDate = x.RegisterDate,
                Inactive = x.DeletedDate.HasValue
            }),
            RecordsTotal = paginationRepositoryResponse.TotalCount,
        };

        return paginationResponse;
    }

    public async Task Update(UpdateAccessGroupRequest request)
    {
        var accessGroup = await uow.AccessGroupRepository.GetById(request.Id) ?? throw new ArgumentException(Globalization.Resources.RecordNotFound);

        accessGroup.Update(request.Name, request.Inactive ? DateTime.Now : null);

        await accessGroup.ValidateAndThrowAsync(validator);

        uow.AccessGroupRepository.Update(accessGroup);

        var accessGroupPermissionsDelete = accessGroup.AccessGroupsPermissions!
            .Where(x => !request.Permissions.Contains(x.Permission));
        if (accessGroupPermissionsDelete.Any())
            uow.AccessGroupPermissionRepository.DeleteRange(accessGroupPermissionsDelete);

        var accessGroupPermissionsInsert = new List<AccessGroupPermission>();
        foreach (var permission in request.Permissions
            .Where(x => !accessGroup.AccessGroupsPermissions!.Any(y => y.Permission == x)))
        {
            var accessGroupPermission = new AccessGroupPermission(accessGroup.Id, permission);
            accessGroupPermissionsInsert.Add(accessGroupPermission);
        }
        if (accessGroupPermissionsInsert.Count != 0)
            await uow.AccessGroupPermissionRepository.AddRangeAsync(accessGroupPermissionsInsert);

        await uow.Commit();
    }
}