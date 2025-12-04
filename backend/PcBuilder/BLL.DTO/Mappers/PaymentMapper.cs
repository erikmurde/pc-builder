using AutoMapper;
using BLL.DTO.Payment;
using DAL.Base;

namespace BLL.DTO.Mappers;

public class PaymentMapper : BaseMapper<PaymentDTO, DAL.DTO.Payment.PaymentDTO>
{
    public PaymentMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public PaymentSimpleDTO MapSimple(DAL.DTO.Payment.PaymentSimpleDTO payment)
    {
        return Mapper.Map<PaymentSimpleDTO>(payment);
    }
    
    public PaymentDetailsDTO MapDetails(DAL.DTO.Payment.PaymentDetailsDTO payment)
    {
        return Mapper.Map<PaymentDetailsDTO>(payment);
    }
    
    public PaymentEditDTO MapEdit(DAL.DTO.Payment.PaymentEditDTO payment)
    {
        return Mapper.Map<PaymentEditDTO>(payment);
    }
    
    public DAL.DTO.Payment.PaymentEditDTO MapEdit(PaymentEditDTO payment)
    {
        return Mapper.Map<DAL.DTO.Payment.PaymentEditDTO>(payment);
    }
    
    public PaymentCreateDTO MapCreate(DAL.DTO.Payment.PaymentCreateDTO payment)
    {
        return Mapper.Map<PaymentCreateDTO>(payment);
    }
    
    public DAL.DTO.Payment.PaymentCreateDTO MapCreate(PaymentCreateDTO payment)
    {
        return Mapper.Map<DAL.DTO.Payment.PaymentCreateDTO>(payment);
    }
}