using BLL.DTO.Attribute;
using BLL.DTO.CartPc;
using BLL.DTO.Manufacturer;
using DTO.Base;
using BLL.DTO.Category;
using BLL.DTO.Discount;
using BLL.DTO.PackageSize;
using BLL.DTO.ShippingMethod;
using BLL.DTO.Status;
using BLL.DTO.UserReview;
using BLL.DTO.Order;
using BLL.DTO.Payment;

namespace Helpers.Base;

/// <summary>
/// Only contains validation for data fields. More complex validation must be done inside services
/// </summary>
public static class EntityValidationHelper
{
    public static bool ValidateCategory(CategoryDTO category)
    {
        return category.CategoryName != "" && category.CategoryName.Length <= 64;
    }

    public static bool ValidateDiscount(DiscountDTO discount)
    {
        if (discount.DiscountName == "" || discount.DiscountName.Length > 128) return false;
        return discount.DiscountPercentage is > 0 and <= 100;
    }
    
    public static bool ValidateManufacturer(ManufacturerDTO manufacturer)
    {
        return manufacturer.ManufacturerName != "" && manufacturer.ManufacturerName.Length <= 128;
    }
    
    public static bool ValidateAttribute(AttributeDTO attribute)
    {
        return attribute.AttributeName != "" && attribute.AttributeName.Length <= 64;
    }
    
    public static bool ValidateComponentAttribute(ComponentAttributeBaseDTO componentAttribute)
    {
        return componentAttribute.AttributeValue != "" && componentAttribute.AttributeValue.Length <= 128;
    }
    
    public static bool ValidateShippingMethod(ShippingMethodDTO method)
    {
        if (method.MethodName == "" || method.MethodName.Length > 64) return false; 
        return method.ShippingTime != "" && method.ShippingTime.Length <= 64;
    }

    public static bool ValidatePackageSize(PackageSizeDTO size)
    {
        return size.SizeName != "" && size.SizeName.Length <= 64;
    }
    
    public static bool ValidateStatus(StatusDTO status)
    {
        if (status.StatusName == "" || status.StatusName.Length > 64) return false;
        return status.Comment is not { Length: > 2048 };
    }
    
    public static bool ValidatePaymentCreate(PaymentCreateDTO payment)
    {
        return payment.PaymentNr != "" && payment.Comment is { Length: < 2048 };
    }
    
    public static bool ValidatePaymentEdit(PaymentEditDTO payment)
    {
        return payment.Comment is { Length: < 2048 };
    }

    public static bool ValidateComponent(ComponentBaseDTO component)
    {
        if (component.ComponentName == "" || component.ComponentName.Length > 128) return false;
        return component.Description != "" && component.Description.Length <= 512;
    }

    public static bool ValidatePcBuild(PcBuildBaseDTO pcBuild)
    {
        if (pcBuild.PcName == "" || pcBuild.Description == "" || pcBuild.ImageSrc is { Length: > 256 })
        {
            return false;
        }
        return pcBuild.Stock is >= 0 and <= 10000;
    }

    public static bool ValidateCartPc(CartPcEditDTO cartPc)
    {
        return cartPc.Qty is > 0 and < 100;
    }

    public static bool ValidateOrderEdit(OrderEditDTO order)
    {
        return order.Comment is { Length: <= 2048};
    }
    
    public static bool ValidateOrderCreate(OrderCreateDTO order)
    {
        if (order.OrderNr == "" || order.OrderNr.Length > 12
            || order.CustomerName == "" || order.CustomerName.Length > 256
            || order.CustomerPhoneNumber == "" || order.CustomerPhoneNumber.Length > 256
            || order.ShippingAddress == "" || order.ShippingAddress.Length > 256
            || order.ShippingPostalCode == "" || order.ShippingPostalCode.Length > 32 
            || order.Comment is { Length: > 2048 }) return false;

        return order.OrderPcData.Count != 0
               && order.OrderShippingCostData.Count != 0
               && order.PaymentData.Count != 0;
    }
    
    public static bool ValidateUserReview(UserReviewEditDTO userReview)
    {
        if (userReview.ReviewContent == "" || userReview.ReviewContent.Length > 2048) return false;
        return userReview.Rating is >= 0 and <= 5;
    }
}