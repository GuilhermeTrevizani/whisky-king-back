using WhiskyKing.Core.Interfaces;
using WhiskyKing.Core.Interfaces.Repositories;

namespace WhiskyKing.Infra.Data;

public class UnitOfWork(
    DatabaseContext databaseContext,
    IAccessGroupPermissionRepository accessGroupPermissionRepository,
    IAccessGroupRepository accessGroupRepository,
    IUserAccessGroupRepository userAccessGroupRepository,
    IUserRepository userRepository,
    ICategoryRepository categoryRepository,
    IPaymentMethodRepository paymentMethodRepository,
    IMerchandiseRepository merchandiseRepository,
    ISaleMerchandiseRepository saleMerchandiseRepository,
    ISalePaymentMethodRepository salePaymentMethodRepository,
    ISaleRepository saleRepository,
    IShiftRepository shiftRepository,
    ICategoryDetailRepository categoryDetailRepository) : IUnitOfWork
{
    public async Task<int> Commit() => await databaseContext.SaveChangesAsync();

    public IAccessGroupPermissionRepository AccessGroupPermissionRepository => accessGroupPermissionRepository;

    public IAccessGroupRepository AccessGroupRepository => accessGroupRepository;

    public IUserAccessGroupRepository UserAccessGroupRepository => userAccessGroupRepository;

    public IUserRepository UserRepository => userRepository;

    public ICategoryRepository CategoryRepository => categoryRepository;

    public IPaymentMethodRepository PaymentMethodRepository => paymentMethodRepository;

    public IMerchandiseRepository MerchandiseRepository => merchandiseRepository;

    public ISaleMerchandiseRepository SaleMerchandiseRepository => saleMerchandiseRepository;

    public ISalePaymentMethodRepository SalePaymentMethodRepository => salePaymentMethodRepository;

    public ISaleRepository SaleRepository => saleRepository;

    public IShiftRepository ShiftRepository => shiftRepository;

    public ICategoryDetailRepository CategoryDetailRepository => categoryDetailRepository;
}