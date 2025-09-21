using WhiskyKing.Core.Interfaces.Repositories;

namespace WhiskyKing.Core.Interfaces;

public interface IUnitOfWork
{
    Task<int> Commit();

    IAccessGroupPermissionRepository AccessGroupPermissionRepository { get; }
    IAccessGroupRepository AccessGroupRepository { get; }
    IUserAccessGroupRepository UserAccessGroupRepository { get; }
    IUserRepository UserRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    IPaymentMethodRepository PaymentMethodRepository { get; }
    IMerchandiseRepository MerchandiseRepository { get; }
    ISaleMerchandiseRepository SaleMerchandiseRepository { get; }
    ISalePaymentMethodRepository SalePaymentMethodRepository { get; }
    ISaleRepository SaleRepository { get; }
    IShiftRepository ShiftRepository { get; }
    ICategoryDetailRepository CategoryDetailRepository { get; }
}