using Microsoft.EntityFrameworkCore;
using WhiskyKing.Core.Models;
using WhiskyKing.Infra.Data;

namespace WhiskyKing.API.Extensions;

public static class ServiceProviderExtension
{
    public static async Task CreateDatabase(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        await databaseContext.Database.MigrateAsync();

        await databaseContext.Database.ExecuteSqlRawAsync($@"INSERT IGNORE INTO Users (Id, Name, Login, Password, RegisterDate, RegisterUserId)
                    VALUES ('{Constants.ID_USER_ADMIN}', 'Admin', 'admin', '$2a$11$8zqZngheoj38RQcKvHxM..1FJRXj6ryygc8QdcERQ76ZrHIjy8Ni2', now(), '{Constants.ID_USER_ADMIN}')");

        await databaseContext.Database.ExecuteSqlRawAsync($@"INSERT IGNORE INTO AccessGroups (Id, Name, RegisterDate, RegisterUserId)
                    VALUES ('{Constants.ID_ACCESS_GROUP_ADMIN}', 'Admin', now(), '{Constants.ID_USER_ADMIN}')");

        await databaseContext.Database.ExecuteSqlRawAsync($@"INSERT IGNORE INTO UsersAccessGroups (Id, UserId, AccessGroupId, RegisterDate, RegisterUserId)
                    VALUES ('A68E1BD9-6434-4ED6-B3E5-F24F87BB6794', '{Constants.ID_USER_ADMIN}', '{Constants.ID_ACCESS_GROUP_ADMIN}', now(), '{Constants.ID_USER_ADMIN}')");
    }
}