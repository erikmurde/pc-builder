using DAL.Contracts.App;
using DAL.DTO.Mappers;
using DAL.DTO.OrderShippingCost;
using DAL.EF.Base;
using Domain.App;

namespace DAL.EF.App.Repositories;

public class OrderShippingCostRepository :
    EFBaseRepository<OrderShippingCostDTO, OrderShippingCost, ApplicationDbContext>, IOrderShippingCostRepository
{
    private readonly OrderShippingCostMapper _mapper;
    
    public OrderShippingCostRepository(ApplicationDbContext dataContext, OrderShippingCostMapper mapper) : 
        base(dataContext, mapper)
    {
        _mapper = mapper;
    }
    
    public OrderShippingCostCreateDTO Add(OrderShippingCostCreateDTO orderShippingCost, Guid orderId)
    {
        var domainOrderShippingCost = _mapper.MapCreate(orderShippingCost);

        domainOrderShippingCost.OrderId = orderId;

        return _mapper.MapCreate(RepositoryDbSet.Add(domainOrderShippingCost).Entity);
    }
}