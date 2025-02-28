namespace WhiskyKing.Domain.Entities;

public class AccessGroup : BaseEntity
{
    public string Name { get; private set; } = string.Empty;

    public ICollection<AccessGroupPermission>? AccessGroupsPermissions { get; private set; }
    public ICollection<UserAccessGroup>? UsersAccessGroups { get; private set; }

    public void Create(string name, ICollection<AccessGroupPermission> accessGroupsPermissions)
    {
        Name = name;
        AccessGroupsPermissions = accessGroupsPermissions;
    }

    public void Update(string name, DateTime? deletedDate)
    {
        Name = name;
        DeletedDate = deletedDate;
    }
}