using BLL.DTO.Payment;
using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IPaymentService : IBaseRepository<PaymentDTO>, 
    IPaymentRepositoryCustom<PaymentDTO, PaymentDetailsDTO, PaymentEditDTO, PaymentCreateDTO>
{
}