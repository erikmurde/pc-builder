using AutoMapper;
using BLL.DTO.Attribute;
using BLL.DTO.CartPc;
using BLL.DTO.Category;
using BLL.DTO.Component;
using BLL.DTO.ComponentAttribute;
using BLL.DTO.Discount;
using BLL.DTO.Identity;
using BLL.DTO.Manufacturer;
using BLL.DTO.Order;
using BLL.DTO.OrderPc;
using BLL.DTO.OrderShippingCost;
using BLL.DTO.PackageSize;
using BLL.DTO.Payment;
using BLL.DTO.PcBuild;
using BLL.DTO.PcComponent;
using BLL.DTO.ShippingCost;
using BLL.DTO.ShippingMethod;
using BLL.DTO.Status;
using BLL.DTO.UserReview;

namespace BLL.DTO;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<StatusDTO, DAL.DTO.Status.StatusDTO>().ReverseMap();
        
        CreateMap<DiscountDTO, DAL.DTO.Discount.DiscountDTO>().ReverseMap();
        
        CreateMap<CategoryDTO, DAL.DTO.Category.CategoryDTO>().ReverseMap();

        CreateMap<AttributeDTO, DAL.DTO.Attribute.AttributeDTO>().ReverseMap();
        
        CreateMap<PackageSizeDTO, DAL.DTO.PackageSize.PackageSizeDTO>().ReverseMap();
        
        CreateMap<ManufacturerDTO, DAL.DTO.Manufacturer.ManufacturerDTO>().ReverseMap();

        CreateMap<ShippingMethodDTO, DAL.DTO.ShippingMethod.ShippingMethodDTO>().ReverseMap();
        
        
        CreateMap<AppRefreshTokenDTO, DAL.DTO.Identity.AppRefreshTokenDTO>().ReverseMap();
        
        
        CreateMap<ShippingCostDTO, DAL.DTO.ShippingCost.ShippingCostDTO>().ReverseMap();
        
        CreateMap<ShippingCostEditDTO, DAL.DTO.ShippingCost.ShippingCostEditDTO>().ReverseMap();
        
        
        CreateMap<PcBuildDTO, DAL.DTO.PcBuild.PcBuildDTO>().ReverseMap();
        
        CreateMap<PcBuildEditDTO, DAL.DTO.PcBuild.PcBuildEditDTO>().ReverseMap();
        
        CreateMap<PcBuildCreateDTO, DAL.DTO.PcBuild.PcBuildCreateDTO>().ReverseMap();

        CreateMap<DAL.DTO.PcBuild.PcBuildDetailsDTO, PcBuildDetailsDTO>();
        
        CreateMap<DAL.DTO.PcBuild.PcBuildCartDTO, PcBuildCartDTO>();
        
        CreateMap<DAL.DTO.PcBuild.PcBuildStoreDTO, PcBuildStoreDTO>();


        CreateMap<PcComponentDTO, DAL.DTO.PcComponent.PcComponentDTO>().ReverseMap();

        CreateMap<DAL.DTO.PcComponent.PcComponentCartDTO, PcComponentCartDTO>();
        
        CreateMap<DAL.DTO.PcComponent.PcComponentStoreDTO, PcComponentStoreDTO>();


        CreateMap<ComponentDTO, DAL.DTO.Component.ComponentDTO>().ReverseMap();
        
        CreateMap<ComponentEditDTO, DAL.DTO.Component.ComponentEditDTO>().ReverseMap();
        
        CreateMap<ComponentCreateDTO, DAL.DTO.Component.ComponentCreateDTO>().ReverseMap();
    
        CreateMap<DAL.DTO.Component.ComponentSimpleDTO, ComponentSimpleDTO>();
        
        CreateMap<DAL.DTO.Component.ComponentDetailsDTO, ComponentDetailsDTO>();
        
        
        CreateMap<ComponentAttributeDTO, DAL.DTO.ComponentAttribute.ComponentAttributeDTO>().ReverseMap();
        
        CreateMap<ComponentAttributeEditDTO, DAL.DTO.ComponentAttribute.ComponentAttributeEditDTO>().ReverseMap();
        
        
        CreateMap<OrderDTO, DAL.DTO.Order.OrderDTO>().ReverseMap();
        
        CreateMap<OrderEditDTO, DAL.DTO.Order.OrderEditDTO>().ReverseMap();
        
        CreateMap<OrderCreateDTO, DAL.DTO.Order.OrderCreateDTO>().ReverseMap();
        
        CreateMap<DAL.DTO.Order.OrderDetailsDTO, OrderDetailsDTO>();
        
        
        CreateMap<OrderPcDTO, DAL.DTO.OrderPc.OrderPcDTO>().ReverseMap();
        
        CreateMap<OrderPcCreateDTO, DAL.DTO.OrderPc.OrderPcCreateDTO>().ReverseMap();
        
        
        CreateMap<OrderShippingCostCreateDTO, DAL.DTO.OrderShippingCost.OrderShippingCostCreateDTO>().ReverseMap();
        
        
        CreateMap<CartPcDTO, DAL.DTO.CartPc.CartPcDTO>().ReverseMap();
        
        CreateMap<CartPcEditDTO, DAL.DTO.CartPc.CartPcDTO>().ReverseMap();


        CreateMap<PaymentDTO, DAL.DTO.Payment.PaymentDTO>().ReverseMap();
        
        CreateMap<PaymentEditDTO, DAL.DTO.Payment.PaymentEditDTO>().ReverseMap();
        
        CreateMap<PaymentCreateDTO, DAL.DTO.Payment.PaymentCreateDTO>().ReverseMap();
    
        CreateMap<DAL.DTO.Payment.PaymentSimpleDTO, PaymentSimpleDTO>();
        
        CreateMap<DAL.DTO.Payment.PaymentDetailsDTO, PaymentDetailsDTO>();
        
        
        CreateMap<UserReviewDTO, DAL.DTO.UserReview.UserReviewDTO>().ReverseMap();
        
        CreateMap<UserReviewEditDTO, DAL.DTO.UserReview.UserReviewEditDTO>().ReverseMap();
    }
}