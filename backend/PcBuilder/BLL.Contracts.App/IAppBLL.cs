using BLL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IAppBLL : IBaseBLL
{
    ICategoryService CategoryService { get; }
    IIdentityService IdentityService { get; }
    IPcBuildService PcBuildService { get; }
    IComponentService ComponentService { get; }
    IDiscountService DiscountService { get; }
    IAttributeService AttributeService { get; }
    IStatusService StatusService { get; }
    IPackageSizeService PackageSizeService { get; }
    IShippingCostService ShippingCostService { get; }
    IShippingMethodService ShippingMethodService { get; }
    IComponentAttributeService ComponentAttributeService { get; }
    IManufacturerService ManufacturerService { get; }
    ICartPcService CartPcService { get; }
    IOrderService OrderService { get; }
    IPaymentService PaymentService { get; }
    IUserReviewService UserReviewService { get; }
}