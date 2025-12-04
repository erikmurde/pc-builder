using DAL.Contracts.Base;

namespace DAL.Contracts.App;

public interface IAppUOW : IBaseUOW
{
    IAttributeRepository AttributeRepository { get; }
    ICartPcRepository CartPcRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    IComponentAttributeRepository ComponentAttributeRepository { get; }
    IComponentRepository ComponentRepository { get; }
    IDiscountRepository DiscountRepository { get; }
    IManufacturerRepository ManufacturerRepository { get; }
    IOrderPcRepository OrderPcRepository { get; }
    IOrderRepository OrderRepository { get; }
    IOrderShippingCostRepository OrderShippingCostRepository { get; }
    IPackageSizeRepository PackageSizeRepository { get; }
    IPaymentRepository PaymentRepository { get; }
    IPcBuildRepository PcBuildRepository { get; }
    IPcComponentRepository PcComponentRepository { get; }
    IShippingCostRepository ShippingCostRepository { get; }
    IShippingMethodRepository ShippingMethodRepository { get; }
    IStatusRepository StatusRepository { get; }
    IUserReviewRepository UserReviewRepository { get; }
    IAppRefreshTokenRepository AppRefreshTokenRepository { get; }
}