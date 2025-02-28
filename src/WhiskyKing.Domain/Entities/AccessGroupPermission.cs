using WhiskyKing.Domain.Enums;

namespace WhiskyKing.Domain.Entities;

public class AccessGroupPermission : BaseEntityMin
{
    public Guid AccessGroupId { get; private set; }
    public Permission Permission { get; private set; }

    public AccessGroup? AccessGroup { get; private set; }

    public void Create(Permission permission)
    {
        Permission = permission;
    }

    public void Create(Guid accessGroupId, Permission permission)
    {
        AccessGroupId = accessGroupId;
        Permission = permission;
    }
}