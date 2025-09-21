namespace WhiskyKing.Domain.Entities;

public class AccessGroup : BaseEntity
{
    private AccessGroup()
    {
    }

    public AccessGroup(string name, ICollection<AccessGroupPermission> accessGroupsPermissions)
    {
        Name = name;
        AccessGroupsPermissions = accessGroupsPermissions;
    }

    public string Name { get; private set; } = default!;

    public ICollection<AccessGroupPermission>? AccessGroupsPermissions { get; private set; }
    public ICollection<UserAccessGroup>? UsersAccessGroups { get; private set; }

    public void Update(string name, DateTime? deletedDate)
    {
        Name = name;
        DeletedDate = deletedDate;
    }
}