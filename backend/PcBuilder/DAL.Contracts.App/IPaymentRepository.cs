using DAL.Contracts.Base;
using DAL.DTO.Payment;

namespace DAL.Contracts.App;

public interface IPaymentRepository : 
    IBaseRepository<PaymentDTO>, IPaymentRepositoryCustom<PaymentDTO, PaymentDetailsDTO, PaymentEditDTO, PaymentCreateDTO>
{
    public Task<IEnumerable<PaymentSimpleDTO>> AllAsyncSimple(Guid orderId);
}

public interface IPaymentRepositoryCustom<TBase, TDetails, TEdit, TCreate>
{
    public Task<IEnumerable<TBase>> AllAsync(Guid userId);
    public Task<TDetails?> FindAsyncDetails(Guid id);
    public Task<TEdit?> FindAsyncEdit(Guid id);
    public Task<TEdit?> FindAsyncEdit(Guid id, Guid userId);
    public TCreate Add(TCreate payment, Guid userId, Guid orderId);
    public Task<TEdit> Update(TEdit payment);
}