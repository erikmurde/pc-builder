using AutoMapper;
using BLL.DTO.CartPc;
using DAL.Base;

namespace BLL.DTO.Mappers;

public class CartPcMapper : BaseMapper<CartPcDTO, DAL.DTO.CartPc.CartPcDTO>
{
    public CartPcMapper(IMapper mapper) : base(mapper)
    {
    }

    public CartPcEditDTO MapEdit(DAL.DTO.CartPc.CartPcDTO cartPc)
    {
        return Mapper.Map<CartPcEditDTO>(cartPc);
    }
    
    public DAL.DTO.CartPc.CartPcDTO MapEdit(CartPcEditDTO cartPc)
    {
        return Mapper.Map<DAL.DTO.CartPc.CartPcDTO>(cartPc);
    }
}