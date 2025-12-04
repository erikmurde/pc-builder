using DAL.Contracts.App;
using DAL.DTO.Mappers;
using DAL.DTO.Payment;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class PaymentRepository : 
    EFBaseRepository<PaymentDTO, Payment, ApplicationDbContext>, IPaymentRepository
{
    private readonly PaymentMapper _mapper;
    
    public PaymentRepository(ApplicationDbContext dataContext, PaymentMapper mapper) : 
        base(dataContext, mapper)
    {
        _mapper = mapper;
    }

    public override async Task<IEnumerable<PaymentDTO>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(p => p.ApplicationUser)
            .Include(p => p.Order)
            .Select(p => new PaymentDTO
            {
                Id = p.Id,
                UserEmail = p.ApplicationUser!.Email!,
                OrderNr = p.Order!.OrderNr,
                PaymentNr = p.PaymentNr,
                AmountPaid = p.AmountPaid,
                PaymentDate = p.PaymentDate
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<PaymentDTO>> AllAsync(Guid userId)
    {
        return await RepositoryDbSet
            .Where(p => p.ApplicationUserId == userId)
            .Include(p => p.ApplicationUser)
            .Include(p => p.Order)
            .Select(p => new PaymentDTO
            {
                Id = p.Id,
                UserEmail = p.ApplicationUser!.Email!,
                OrderNr = p.Order!.OrderNr,
                PaymentNr = p.PaymentNr,
                AmountPaid = p.AmountPaid,
                PaymentDate = p.PaymentDate
            })
            .ToListAsync();
    }
    
    public async Task<IEnumerable<PaymentSimpleDTO>> AllAsyncSimple(Guid orderId)
    {
        return await RepositoryDbSet
            .Where(p => p.OrderId == orderId)
            .Select(p => new PaymentSimpleDTO
            {
                PaymentNr = p.PaymentNr,
                AmountPaid = p.AmountPaid,
                PaymentDate = p.PaymentDate
            })
            .ToListAsync();
    }

    public async Task<PaymentDetailsDTO?> FindAsyncDetails(Guid id)
    {
        return await RepositoryDbSet
            .Where(p => p.Id == id)
            .Include(p => p.ApplicationUser)
            .Include(p => p.Order)
            .Select(p => new PaymentDetailsDTO
            {
                Id = p.Id,
                CustomerName = p.Order!.CustomerName,
                UserEmail = p.ApplicationUser!.Email!,
                OrderNr = p.Order.OrderNr,
                PaymentNr = p.PaymentNr,
                AmountPaid = p.AmountPaid,
                PaymentDate = p.PaymentDate,
                Comment = p.Comment
            })
            .FirstOrDefaultAsync();
    }
    
    public async Task<PaymentEditDTO?> FindAsyncEdit(Guid id)
    {
        return await RepositoryDbSet
            .Where(p => p.Id == id)
            .Select(p => new PaymentEditDTO
            {
                Id = p.Id,
                Comment = p.Comment ?? ""
            })
            .FirstOrDefaultAsync();
    }
    
    public async Task<PaymentEditDTO?> FindAsyncEdit(Guid id, Guid userId)
    {
        return await RepositoryDbSet
            .Where(p => p.Id == id && p.ApplicationUserId == userId)
            .Select(p => new PaymentEditDTO
            {
                Id = p.Id,
                Comment = p.Comment ?? ""
            })
            .FirstOrDefaultAsync();
    }
    
    public async Task<PaymentEditDTO> Update(PaymentEditDTO payment)
    {
        var domainPayment = await RepositoryDbSet.FindAsync(payment.Id);

        domainPayment!.Comment = payment.Comment;
        
        return _mapper.MapEdit(RepositoryDbSet.Update(domainPayment).Entity);
    }

    public PaymentCreateDTO Add(PaymentCreateDTO payment, Guid userId, Guid orderId)
    {
        var domainPayment = _mapper.MapCreate(payment);
        
        domainPayment.ApplicationUserId = userId;
        domainPayment.OrderId = orderId;
        domainPayment.PaymentDate = DateTime.UtcNow;

        return _mapper.MapCreate(RepositoryDbSet.Add(domainPayment).Entity);
    }
}