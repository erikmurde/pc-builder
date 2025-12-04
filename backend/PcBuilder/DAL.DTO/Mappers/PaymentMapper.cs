using AutoMapper;
using DAL.Base;
using DAL.DTO.Payment;

namespace DAL.DTO.Mappers;

public class PaymentMapper : BaseMapper<PaymentDTO, Domain.App.Payment>
{
    public PaymentMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public PaymentEditDTO MapEdit(Domain.App.Payment payment)
    {
        return Mapper.Map<PaymentEditDTO>(payment);
    }
    
    public Domain.App.Payment MapEdit(PaymentEditDTO payment)
    {
        return Mapper.Map<Domain.App.Payment>(payment);
    }
    
    public PaymentCreateDTO MapCreate(Domain.App.Payment payment)
    {
        return Mapper.Map<PaymentCreateDTO>(payment);
    }
    
    public Domain.App.Payment MapCreate(PaymentCreateDTO payment)
    {
        return Mapper.Map<Domain.App.Payment>(payment);
    }
}