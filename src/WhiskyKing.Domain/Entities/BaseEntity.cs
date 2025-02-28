namespace WhiskyKing.Domain.Entities;

public abstract class BaseEntity : BaseEntityMin
{
    public DateTime? LastChangeDate { get; internal set; }
    public Guid? LastChangeUserId { get; internal set; }
    public DateTime? DeletedDate { get; internal set; }

    public User? LastChangeUser { get; private set; }

    public void SetLastChangeUser(Guid lastChangeUserId)
    {
        LastChangeDate = DateTime.Now;
        LastChangeUserId = lastChangeUserId;
    }
}