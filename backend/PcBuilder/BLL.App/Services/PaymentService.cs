using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO.Payment;
using BLL.DTO.Mappers;
using DAL.Contracts.App;
using Helpers.Base;

namespace BLL.App.Services;

public class PaymentService : 
    BaseEntityService<PaymentDTO, DAL.DTO.Payment.PaymentDTO, IPaymentRepository>, IPaymentService
{
    private readonly IAppUOW _uow;
    private readonly PaymentMapper _mapper;
    
    public PaymentService(IAppUOW uow, PaymentMapper mapper) : 
        base(uow.PaymentRepository, mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PaymentDTO>> AllAsync(Guid userId)
    {
        return (await _uow.PaymentRepository.AllAsync(userId))
            .Select(p => _mapper.Map(p));
    }

    public async Task<PaymentDetailsDTO?> FindAsyncDetails(Guid id)
    {
        var dalPayment = await _uow.PaymentRepository.FindAsyncDetails(id);
        return dalPayment == null ? null : _mapper.MapDetails(dalPayment);
    }

    public async Task<PaymentEditDTO?> FindAsyncEdit(Guid id)
    {
        var dalPayment = await _uow.PaymentRepository.FindAsyncEdit(id);
        return dalPayment == null ? null : _mapper.MapEdit(dalPayment);
    }

    public async Task<PaymentEditDTO?> FindAsyncEdit(Guid id, Guid userId)
    {
        var dalPayment = await _uow.PaymentRepository.FindAsyncEdit(id, userId);
        return dalPayment == null ? null : _mapper.MapEdit(dalPayment);
    }

    public async Task<PaymentEditDTO> Update(PaymentEditDTO payment)
    {
        if (!EntityValidationHelper.ValidatePaymentEdit(payment))
        {
            throw new ArgumentException("");
        }

        var dalPayment = _mapper.MapEdit(payment);
        return _mapper.MapEdit(await _uow.PaymentRepository.Update(dalPayment));
    }

    public PaymentCreateDTO Add(PaymentCreateDTO payment, Guid userId, Guid orderId)
    {
        if (!EntityValidationHelper.ValidatePaymentCreate(payment))
        {
            throw new ArgumentException("");
        }

        var dalPayment = _mapper.MapCreate(payment);
        return _mapper.MapCreate(_uow.PaymentRepository.Add(dalPayment, userId, orderId));
    }
}