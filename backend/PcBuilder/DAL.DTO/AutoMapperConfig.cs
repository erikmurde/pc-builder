using AutoMapper;
using DAL.DTO.Attribute;
using DAL.DTO.CartPc;
using DAL.DTO.Category;
using DAL.DTO.Component;
using DAL.DTO.ComponentAttribute;
using DAL.DTO.Discount;
using DAL.DTO.Identity;
using DAL.DTO.Manufacturer;
using DAL.DTO.Order;
using DAL.DTO.OrderPc;
using DAL.DTO.OrderShippingCost;
using DAL.DTO.PackageSize;
using DAL.DTO.Payment;
using DAL.DTO.PcBuild;
using DAL.DTO.PcComponent;
using DAL.DTO.ShippingCost;
using DAL.DTO.ShippingMethod;
using DAL.DTO.Status;
using DAL.DTO.UserReview;

namespace DAL.DTO;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<AttributeDTO, Domain.App.Attribute>().ReverseMap();

        CreateMap<CartPcDTO, Domain.App.CartPc>().ReverseMap();

        CreateMap<CategoryDTO, Domain.App.Category>().ReverseMap();

        CreateMap<ComponentDTO, Domain.App.Component>().ReverseMap();
        
        CreateMap<ComponentCreateDTO, Domain.App.Component>().ReverseMap();

        CreateMap<ComponentEditDTO, Domain.App.Component>().ReverseMap();
        
        
        CreateMap<ComponentAttributeDTO, Domain.App.ComponentAttribute>().ReverseMap();
        
        CreateMap<ComponentAttributeEditDTO, Domain.App.ComponentAttribute>().ReverseMap();
        

        CreateMap<DiscountDTO, Domain.App.Discount>().ReverseMap();
        
        CreateMap<AppRefreshTokenDTO, Domain.App.Identity.AppRefreshToken>().ReverseMap();
        
        CreateMap<ManufacturerDTO, Domain.App.Manufacturer>().ReverseMap();


        CreateMap<OrderDTO, Domain.App.Order>().ReverseMap();
            
        CreateMap<OrderUpdateDTO, Domain.App.Order>().ReverseMap();

        CreateMap<OrderCreateDTO, Domain.App.Order>().ReverseMap();
        
        CreateMap<Domain.App.Order, OrderEditDTO>();
        
        
        CreateMap<OrderPcDTO, Domain.App.OrderPc>().ReverseMap();
        
        CreateMap<OrderPcCreateDTO, Domain.App.OrderPc>().ReverseMap();
        
        
        CreateMap<OrderShippingCostDTO, Domain.App.OrderShippingCost>().ReverseMap();
        
        CreateMap<OrderShippingCostCreateDTO, Domain.App.OrderShippingCost>().ReverseMap();
        
        
        CreateMap<PackageSizeDTO, Domain.App.PackageSize>().ReverseMap();
        
        
        CreateMap<PaymentDTO, Domain.App.Payment>().ReverseMap();
        
        CreateMap<PaymentEditDTO, Domain.App.Payment>().ReverseMap();
        
        CreateMap<PaymentCreateDTO, Domain.App.Payment>().ReverseMap();


        CreateMap<PcBuildDTO, Domain.App.PcBuild>().ReverseMap();
        
        CreateMap<PcBuildEditDTO, Domain.App.PcBuild>().ReverseMap();
        
        CreateMap<PcBuildCreateDTO, Domain.App.PcBuild>().ReverseMap();
        
        
        CreateMap<PcComponentDTO, Domain.App.PcComponent>().ReverseMap();
        
        CreateMap<PcComponentCreateDTO, Domain.App.PcComponent>().ReverseMap();
        
        CreateMap<PcComponentEditDTO, Domain.App.PcComponent>().ReverseMap();


        CreateMap<ShippingCostDTO, Domain.App.ShippingCost>().ReverseMap();
        
        CreateMap<ShippingCostEditDTO, Domain.App.ShippingCost>().ReverseMap();
        
        
        CreateMap<ShippingMethodDTO, Domain.App.ShippingMethod>().ReverseMap();
        
        CreateMap<StatusDTO, Domain.App.Status>().ReverseMap();
        
        CreateMap<UserReviewDTO, Domain.App.UserReview>().ReverseMap();
        
        
        CreateMap<UserReviewDTO, Domain.App.UserReview>().ReverseMap();
        
        CreateMap<UserReviewEditDTO, Domain.App.UserReview>().ReverseMap();
    }
}