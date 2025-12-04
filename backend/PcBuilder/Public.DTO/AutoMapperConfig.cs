using AutoMapper;
using Public.DTO.V1.Attribute;
using Public.DTO.V1.CartPc;
using Public.DTO.V1.Category;
using Public.DTO.V1.Component;
using Public.DTO.V1.ComponentAttributes;
using Public.DTO.V1.Discount;
using Public.DTO.V1.Manufacturer;
using Public.DTO.V1.Order;
using Public.DTO.V1.OrderPC;
using Public.DTO.V1.OrderShippingCost;
using Public.DTO.V1.PackageSize;
using Public.DTO.V1.Payment;
using Public.DTO.V1.PcBuild;
using Public.DTO.V1.PcComponent;
using Public.DTO.V1.ShippingCost;
using Public.DTO.V1.ShippingMethod;
using Public.DTO.V1.Status;
using Public.DTO.V1.UserReview;

namespace Public.DTO;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<BLL.DTO.Category.CategoryDTO, CategoryDTO>().ReverseMap();
            
        CreateMap<CategoryCreateDTO, BLL.DTO.Category.CategoryDTO>();
        

        CreateMap<BLL.DTO.Discount.DiscountDTO, DiscountDTO>().ReverseMap();
            
        CreateMap<DiscountCreateDTO, BLL.DTO.Discount.DiscountDTO>();
        
        
        CreateMap<BLL.DTO.Manufacturer.ManufacturerDTO, ManufacturerDTO>().ReverseMap();

        CreateMap<ManufacturerCreateDTO, BLL.DTO.Manufacturer.ManufacturerDTO>().ReverseMap();
        

        CreateMap<BLL.DTO.Attribute.AttributeDTO, AttributeDTO>().ReverseMap();

        CreateMap<AttributeCreateDTO, BLL.DTO.Attribute.AttributeDTO>();
        

        CreateMap<BLL.DTO.ShippingMethod.ShippingMethodDTO, ShippingMethodDTO>().ReverseMap();

        CreateMap<ShippingMethodCreateDTO, BLL.DTO.ShippingMethod.ShippingMethodDTO>();
        
        
        CreateMap<BLL.DTO.PackageSize.PackageSizeDTO, PackageSizeDTO>().ReverseMap();

        CreateMap<PackageSizeCreateDTO, BLL.DTO.PackageSize.PackageSizeDTO>();
        
        
        CreateMap<BLL.DTO.Status.StatusDTO, StatusDTO>().ReverseMap();

        CreateMap<StatusCreateDTO, BLL.DTO.Status.StatusDTO>();
        

        CreateMap<CartPcDTO, BLL.DTO.CartPc.CartPcDTO>().ReverseMap();
        
        CreateMap<CartPcCreateDTO, BLL.DTO.CartPc.CartPcEditDTO>();
        
        CreateMap<CartPcEditDTO, BLL.DTO.CartPc.CartPcEditDTO>();


        CreateMap<PcBuildDTO, BLL.DTO.PcBuild.PcBuildDTO>().ReverseMap();
        
        CreateMap<PcBuildEditDTO, BLL.DTO.PcBuild.PcBuildEditDTO>().ReverseMap();
        
        CreateMap<PcBuildCreateDTO, BLL.DTO.PcBuild.PcBuildCreateDTO>();
        
        CreateMap<BLL.DTO.PcBuild.PcBuildDetailsDTO, PcBuildDetailsDTO>();
        
        CreateMap<BLL.DTO.PcBuild.PcBuildCartDTO, PcBuildCartDTO>();
        
        CreateMap<BLL.DTO.PcBuild.PcBuildStoreDTO, PcBuildStoreDTO>();


        CreateMap<PcComponentDTO, BLL.DTO.PcComponent.PcComponentDTO>().ReverseMap();

        CreateMap<BLL.DTO.PcComponent.PcComponentCartDTO, PcComponentCartDTO>();
        
        CreateMap<BLL.DTO.PcComponent.PcComponentStoreDTO, PcComponentStoreDTO>();
        

        CreateMap<PaymentDTO, BLL.DTO.Payment.PaymentDTO>().ReverseMap();
        
        CreateMap<PaymentEditDTO, BLL.DTO.Payment.PaymentEditDTO>().ReverseMap();
        
        CreateMap<PaymentCreateDTO, BLL.DTO.Payment.PaymentCreateDTO>();
        
        CreateMap<BLL.DTO.Payment.PaymentSimpleDTO, PaymentSimpleDTO>();
        
        CreateMap<BLL.DTO.Payment.PaymentDetailsDTO, PaymentDetailsDTO>();


        CreateMap<OrderDTO, BLL.DTO.Order.OrderDTO>().ReverseMap();
        
        CreateMap<OrderEditDTO, BLL.DTO.Order.OrderEditDTO>().ReverseMap();
        
        CreateMap<BLL.DTO.Order.OrderDetailsDTO, OrderDetailsDTO>();

        CreateMap<OrderCreateDTO, BLL.DTO.Order.OrderCreateDTO>();
        

        CreateMap<OrderPcDTO, BLL.DTO.OrderPc.OrderPcDTO>().ReverseMap();
        
        CreateMap<OrderPcCreateDTO, BLL.DTO.OrderPc.OrderPcCreateDTO>().ReverseMap();
        
        
        CreateMap<ShippingCostDTO, BLL.DTO.ShippingCost.ShippingCostDTO>().ReverseMap();
        
        CreateMap<ShippingCostEditDTO, BLL.DTO.ShippingCost.ShippingCostEditDTO>().ReverseMap();
        
        CreateMap<ShippingCostCreateDTO, BLL.DTO.ShippingCost.ShippingCostEditDTO>();


        CreateMap<OrderShippingCostCreateDTO, BLL.DTO.OrderShippingCost.OrderShippingCostCreateDTO>();
        
        
        CreateMap<ComponentAttributeDTO, BLL.DTO.ComponentAttribute.ComponentAttributeDTO>().ReverseMap();
        
        CreateMap<ComponentAttributeEditDTO, BLL.DTO.ComponentAttribute.ComponentAttributeEditDTO>().ReverseMap();
        
        CreateMap<ComponentAttributeCreateDTO, BLL.DTO.ComponentAttribute.ComponentAttributeEditDTO>();


        CreateMap<UserReviewDTO, BLL.DTO.UserReview.UserReviewDTO>().ReverseMap();
        
        CreateMap<UserReviewCreateDTO, BLL.DTO.UserReview.UserReviewEditDTO>();
        
        CreateMap<UserReviewEditDTO, BLL.DTO.UserReview.UserReviewEditDTO>();
        
        
        CreateMap<ComponentDTO, BLL.DTO.Component.ComponentDTO>().ReverseMap();

        CreateMap<ComponentEditDTO, BLL.DTO.Component.ComponentEditDTO>().ReverseMap();
        
        CreateMap<ComponentCreateDTO, BLL.DTO.Component.ComponentCreateDTO>();
        
        CreateMap<BLL.DTO.Component.ComponentDetailsDTO, ComponentDetailsDTO>();
        
        CreateMap<BLL.DTO.Component.ComponentSimpleDTO, ComponentSimpleDTO>();
    }
}