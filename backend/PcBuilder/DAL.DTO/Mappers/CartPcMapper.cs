using AutoMapper;
using DAL.Base;
using DAL.DTO.CartPc;

namespace DAL.DTO.Mappers;

public class CartPcMapper : BaseMapper<CartPcDTO, Domain.App.CartPc>
{
    public CartPcMapper(IMapper mapper) : base(mapper)
    {
    }
}