namespace WhiskyKing.Domain.Entities;

public class User : BaseEntity
{
    private User()
    {
    }

    public User(string name, string login, string password, ICollection<UserAccessGroup> usersAccessGroups)
    {
        Name = name;
        Login = login;
        Password = password;
        UsersAccessGroups = usersAccessGroups;
    }

    public string Name { get; private set; } = default!;
    public string Login { get; private set; } = default!;
    public string Password { get; private set; } = default!;

    public ICollection<UserAccessGroup>? UsersAccessGroups { get; private set; }

    public void ChangePassword(string password)
    {
        Password = password;
    }

    public void Update(string name, string login, DateTime? deletedDate)
    {
        Name = name;
        Login = login;
        DeletedDate = deletedDate;
    }
}