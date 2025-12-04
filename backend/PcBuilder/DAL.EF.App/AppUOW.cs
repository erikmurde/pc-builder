using AutoMapper;
using DAL.Contracts.App;
using DAL.DTO.Mappers;
using DAL.EF.App.Repositories;
using DAL.EF.Base;

namespace DAL.EF.App;

public class AppUOW : EFBaseUOW<ApplicationDbContext>, IAppUOW
{
    private readonly IMapper _mapper;
    
    public AppUOW(ApplicationDbContext dataContext, IMapper mapper) : base(dataContext)
    {
        _mapper = mapper;
    }
    
    private IAttributeRepository? _attributeRepository;
    private ICartPcRepository? _cartPcRepository;
    private ICategoryRepository? _categoryRepository;
    private IComponentAttributeRepository? _componentAttributeRepository;
    private IComponentRepository? _componentRepository;
    private IDiscountRepository? _discountRepository;
    private IManufacturerRepository? _manufacturerRepository;
    private IOrderPcRepository? _orderPcRepository;
    private IOrderRepository? _orderRepository;
    private IOrderShippingCostRepository? _orderShippingCostRepository;
    private IPackageSizeRepository? _packageSizeRepository;
    private IPaymentRepository? _paymentRepository;
    private IPcBuildRepository? _pcBuildRepository;
    private IPcComponentRepository? _pcComponentRepository;
    private IShippingCostRepository? _shippingCostRepository;
    private IShippingMethodRepository? _shippingMethodRepository;
    private IStatusRepository? _statusRepository;
    private IUserReviewRepository? _userReviewRepository;
    private IAppRefreshTokenRepository? _appRefreshTokenRepository;

    public IAttributeRepository AttributeRepository =>
        _attributeRepository ??= new AttributeRepository(UowDbContext, new AttributeMapper(_mapper));
    public ICartPcRepository CartPcRepository => 
        _cartPcRepository ??= new CartPcRepository(UowDbContext, new CartPcMapper(_mapper));
    public ICategoryRepository CategoryRepository => 
        _categoryRepository ??= new CategoryRepository(UowDbContext, new CategoryMapper(_mapper));
    public IComponentAttributeRepository ComponentAttributeRepository =>
        _componentAttributeRepository ??= new ComponentAttributeRepository(UowDbContext, new ComponentAttributeMapper(_mapper));
    public IComponentRepository ComponentRepository => 
        _componentRepository ??= new ComponentRepository(UowDbContext, new ComponentMapper(_mapper));
    public IDiscountRepository DiscountRepository => 
        _discountRepository ??= new DiscountRepository(UowDbContext, new DiscountMapper(_mapper));
    public IManufacturerRepository ManufacturerRepository =>
        _manufacturerRepository ??= new ManufacturerRepository(UowDbContext, new ManufacturerMapper(_mapper));
    public IOrderPcRepository OrderPcRepository => 
        _orderPcRepository ??= new OrderPcRepository(UowDbContext, new OrderPcMapper(_mapper));
    public IOrderRepository OrderRepository => 
        _orderRepository ??= new OrderRepository(UowDbContext, new OrderMapper(_mapper));
    public IOrderShippingCostRepository OrderShippingCostRepository =>
        _orderShippingCostRepository ??= new OrderShippingCostRepository(UowDbContext, new OrderShippingCostMapper(_mapper));
    public IPackageSizeRepository PackageSizeRepository =>
        _packageSizeRepository ??= new PackageSizeRepository(UowDbContext, new PackageSizeMapper(_mapper));
    public IPaymentRepository PaymentRepository => 
        _paymentRepository ??= new PaymentRepository(UowDbContext, new PaymentMapper(_mapper));
    public IPcBuildRepository PcBuildRepository => 
        _pcBuildRepository ??= new PcBuildRepository(UowDbContext, new PcBuildMapper(_mapper));
    public IPcComponentRepository PcComponentRepository =>
        _pcComponentRepository ??= new PcComponentRepository(UowDbContext, new PcComponentMapper(_mapper));
    public IShippingCostRepository ShippingCostRepository =>
        _shippingCostRepository ??= new ShippingCostRepository(UowDbContext, new ShippingCostMapper(_mapper));
    public IShippingMethodRepository ShippingMethodRepository =>
        _shippingMethodRepository ??= new ShippingMethodRepository(UowDbContext, new ShippingMethodMapper(_mapper));
    public IStatusRepository StatusRepository => 
        _statusRepository ??= new StatusRepository(UowDbContext, new StatusMapper(_mapper));
    public IUserReviewRepository UserReviewRepository => 
        _userReviewRepository ??= new UserReviewRepository(UowDbContext, new UserReviewMapper(_mapper));
    public IAppRefreshTokenRepository AppRefreshTokenRepository =>
        _appRefreshTokenRepository ??= new AppRefreshTokenRepository(UowDbContext, new IdentityMapper(_mapper));
}