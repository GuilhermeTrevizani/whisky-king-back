namespace WhiskyKing.Domain.Entities;

public class UserAccessGroup : BaseEntityMin
{
    public Guid UserId { get; private set; }
    public Guid AccessGroupId { get; private set; }

    public User? User { get; private set; }
    public AccessGroup? AccessGroup { get; private set; }

    public void Create(Guid accessGroupId)
    {
        AccessGroupId = accessGroupId;
    }

    public void Create(Guid userId, Guid accessGroupId)
    {
        UserId = userId;
        AccessGroupId = accessGroupId;
    }
}