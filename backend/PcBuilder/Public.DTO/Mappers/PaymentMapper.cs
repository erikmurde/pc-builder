using AutoMapper;
using DAL.Base;
using Public.DTO.V1.Payment;

namespace Public.DTO.Mappers;

public class PaymentMapper : BaseMapper<PaymentDTO, BLL.DTO.Payment.PaymentDTO>
{
    public PaymentMapper(IMapper mapper) : base(mapper)
    {
    }

    public PaymentDetailsDTO MapDetails(BLL.DTO.Payment.PaymentDetailsDTO payment)
    {
        return Mapper.Map<PaymentDetailsDTO>(payment);
    }

    public PaymentEditDTO MapEdit(BLL.DTO.Payment.PaymentEditDTO payment)
    {
        return Mapper.Map<PaymentEditDTO>(payment);
    }
    
    public BLL.DTO.Payment.PaymentEditDTO MapEdit(PaymentEditDTO payment)
    {
        return Mapper.Map<BLL.DTO.Payment.PaymentEditDTO>(payment);
    }
}