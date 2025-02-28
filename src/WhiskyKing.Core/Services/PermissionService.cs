using WhiskyKing.Core.Interfaces.Services;
using WhiskyKing.Core.Models.Responses;
using WhiskyKing.Domain.Enums;

namespace WhiskyKing.Core.Services;

public class PermissionService : IPermissionService
{
    public IEnumerable<PermissionResponse> GetAll()
    {
        return Enum.GetValues<Permission>()
            .Select(x => new PermissionResponse
            {
                Id = x,
                Name = GetDescription(x),
            })
            .OrderBy(x => x.Name);
    }

    private static string GetDescription(Permission permission)
    {
        return permission switch
        {
            Permission.ViewUsers => Globalization.Resources.ViewUsers,
            Permission.ManageUsers => Globalization.Resources.ManageUsers,
            Permission.ViewAccessGroups => Globalization.Resources.ViewAccessGroups,
            Permission.ManageAccessGroups => Globalization.Resources.ManageAccessGroups,
            Permission.ViewCategories => Globalization.Resources.ViewCategories,
            Permission.ManageCategories => Globalization.Resources.ManageCategories,
            Permission.ViewPaymentMethods => Globalization.Resources.ViewPaymentMethods,
            Permission.ManagePaymentMethods => Globalization.Resources.ManagePaymentMethods,
            Permission.ViewMerchandises => Globalization.Resources.ViewMerchandises,
            Permission.ManageMerchandises => Globalization.Resources.ManageMerchandises,
            Permission.ViewSales => Globalization.Resources.ViewSales,
            Permission.ManageSales => Globalization.Resources.ManageSales,
            _ => permission.ToString(),
        };
    }
}