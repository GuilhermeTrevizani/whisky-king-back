namespace WhiskyKing.Domain.Entities;

public class Shift : BaseEntityMin
{
    public DateTime? LastChangeDate { get; private set; }
    public Guid? LastChangeUserId { get; private set; }

    public User? LastChangeUser { get; private set; }
}