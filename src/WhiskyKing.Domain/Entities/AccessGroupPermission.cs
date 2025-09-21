using WhiskyKing.Domain.Enums;

namespace WhiskyKing.Domain.Entities;

public class AccessGroupPermission : BaseEntityMin
{
    private AccessGroupPermission()
    {
    }

    public AccessGroupPermission(Permission permission)
    {
        Permission = permission;
    }

    public AccessGroupPermission(Guid accessGroupId, Permission permission)
    {
        AccessGroupId = accessGroupId;
        Permission = permission;
    }

    public Guid AccessGroupId { get; private set; }
    public Permission Permission { get; private set; }

    public AccessGroup? AccessGroup { get; private set; }
}