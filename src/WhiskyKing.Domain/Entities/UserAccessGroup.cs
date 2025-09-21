namespace WhiskyKing.Domain.Entities;

public class UserAccessGroup : BaseEntityMin
{
    private UserAccessGroup()
    {
    }

    public UserAccessGroup(Guid accessGroupId)
    {
        AccessGroupId = accessGroupId;
    }

    public UserAccessGroup(Guid userId, Guid accessGroupId)
    {
        UserId = userId;
        AccessGroupId = accessGroupId;
    }

    public Guid UserId { get; private set; }
    public Guid AccessGroupId { get; private set; }

    public User? User { get; private set; }
    public AccessGroup? AccessGroup { get; private set; }
}