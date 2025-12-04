using AutoMapper;
using BLL.App.Services;
using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO.Mappers;
using DAL.Contracts.App;

namespace BLL.App;

public class AppBLL: BaseBLL<IAppUOW>, IAppBLL
{
    private readonly IAppUOW _uow;
    private readonly IMapper _mapper;

    public AppBLL(IAppUOW uow, IMapper mapper) : base(uow)
    {
        _uow = uow;
        _mapper = mapper;
    }
    
    private ICategoryService? _categoryService;
    private IIdentityService? _identityService;
    private PcBuildService? _pcBuildService;
    private ComponentService? _componentService;
    private DiscountService? _discountService;
    private AttributeService? _attributeService;
    private StatusService? _statusService;
    private PackageSizeService? _packageSizeService;
    private ShippingCostService? _shippingCostService;
    private ShippingMethodService? _shippingMethodService;
    private ComponentAttributeService? _componentAttributeService;
    private ManufacturerService? _manufacturerService;
    private CartPcService? _cartPcService;
    private OrderService? _orderService;
    private PaymentService? _paymentService;
    private UserReviewService? _userReviewService;

    public ICategoryService CategoryService => 
        _categoryService ??= new CategoryService(_uow, new CategoryMapper(_mapper));
    public IIdentityService IdentityService =>
        _identityService ??= new IdentityService(_uow, new IdentityMapper(_mapper));
    public IPcBuildService PcBuildService => 
        _pcBuildService ??= new PcBuildService(
            _uow, new PcBuildMapper(_mapper), new PcComponentMapper(_mapper), new UserReviewMapper(_mapper));
    public IComponentService ComponentService =>
        _componentService ??= new ComponentService(
            _uow, new ComponentMapper(_mapper), new ComponentAttributeMapper(_mapper));
    public IDiscountService DiscountService =>
        _discountService ??= new DiscountService(_uow, new DiscountMapper(_mapper));
    public IAttributeService AttributeService =>
        _attributeService ??= new AttributeService(_uow, new AttributeMapper(_mapper));
    public IStatusService StatusService =>
        _statusService ??= new StatusService(_uow, new StatusMapper(_mapper));
    public IPackageSizeService PackageSizeService =>
        _packageSizeService ??= new PackageSizeService(_uow, new PackageSizeMapper(_mapper));
    public IShippingCostService ShippingCostService =>
        _shippingCostService ??= new ShippingCostService(_uow, new ShippingCostMapper(_mapper));
    public IShippingMethodService ShippingMethodService =>
        _shippingMethodService ??= new ShippingMethodService(_uow, new ShippingMethodMapper(_mapper));
    public IComponentAttributeService ComponentAttributeService =>
        _componentAttributeService ??= new ComponentAttributeService(_uow, new ComponentAttributeMapper(_mapper));
    public IManufacturerService ManufacturerService =>
        _manufacturerService ??= new ManufacturerService(_uow, new ManufacturerMapper(_mapper));
    public ICartPcService CartPcService =>
        _cartPcService ??= new CartPcService(
            _uow, new CartPcMapper(_mapper), new PcBuildMapper(_mapper), new PcComponentMapper(_mapper));
    public IOrderService OrderService =>
        _orderService ??= new OrderService(_uow, new OrderMapper(_mapper), new OrderPcMapper(_mapper), 
            new PaymentMapper(_mapper), new PcBuildMapper(_mapper), new OrderShippingCostMapper(_mapper));
    public IPaymentService PaymentService =>
        _paymentService ??= new PaymentService(_uow, new PaymentMapper(_mapper));
    public IUserReviewService UserReviewService =>
        _userReviewService ??= new UserReviewService(_uow, new UserReviewMapper(_mapper));
}