using AutoMapper;
using DAL.Base;
using Public.DTO.V1.CartPc;

namespace Public.DTO.Mappers;

public class CartPcMapper : BaseMapper<CartPcDTO, BLL.DTO.CartPc.CartPcDTO>
{
    public CartPcMapper(IMapper mapper) : base(mapper)
    {
    }

    public BLL.DTO.CartPc.CartPcEditDTO MapCreate(CartPcCreateDTO cartPc)
    {
        return Mapper.Map<BLL.DTO.CartPc.CartPcEditDTO>(cartPc);
    }

    public BLL.DTO.CartPc.CartPcEditDTO MapEdit(CartPcEditDTO cartPc)
    {
        return Mapper.Map<BLL.DTO.CartPc.CartPcEditDTO>(cartPc);
    }
}