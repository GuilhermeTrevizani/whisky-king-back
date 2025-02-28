namespace WhiskyKing.Domain.Entities;

public abstract class BaseEntityMin
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public DateTime RegisterDate { get; private set; } = DateTime.Now;
    public Guid RegisterUserId { get; private set; }

    public User? RegisterUser { get; private set; }

    public void SetRegisterUser(Guid registerUserId)
    {
        RegisterUserId = registerUserId;
    }
}