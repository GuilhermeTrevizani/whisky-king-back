namespace WhiskyKing.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Login { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;

    public ICollection<UserAccessGroup>? UsersAccessGroups { get; private set; }

    public void Create(string name, string login, string password, ICollection<UserAccessGroup> usersAccessGroups)
    {
        Name = name;
        Login = login;
        Password = password;
        UsersAccessGroups = usersAccessGroups;
    }

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