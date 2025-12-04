using DAL.Contracts.App;
using DAL.DTO.Mappers;
using DAL.DTO.OrderPc;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class OrderPcRepository : 
    EFBaseRepository<OrderPcDTO, OrderPc, ApplicationDbContext>, IOrderPcRepository
{
    private readonly OrderPcMapper _mapper;
    
    public OrderPcRepository(ApplicationDbContext dataContext, OrderPcMapper mapper) : 
        base(dataContext, mapper)
    {
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderPcDTO>> AllAsync(Guid orderId)
    {
        return await RepositoryDbSet
            .Where(o => o.OrderId == orderId)
            .Include(o => o.PackageSize)
            .Select(o => new OrderPcDTO
            {
                PcBuildId = o.PcBuildId,
                PackageSize = o.PackageSize!.SizeName,
                PricePerUnit = o.PricePerUnit,
                Qty = o.Qty
            })
            .ToListAsync();
    }

    public OrderPcCreateDTO Add(OrderPcCreateDTO orderPc, Guid orderId)
    {
        var domainOrderPc = _mapper.MapCreate(orderPc);

        domainOrderPc.OrderId = orderId;

        return _mapper.MapCreate(RepositoryDbSet.Add(domainOrderPc).Entity);
    }
}