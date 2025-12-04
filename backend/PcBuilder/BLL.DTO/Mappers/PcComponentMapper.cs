using AutoMapper;
using DAL.Base;
using BLL.DTO.PcComponent;

namespace BLL.DTO.Mappers;

public class PcComponentMapper : BaseMapper<PcComponentDTO, DAL.DTO.PcComponent.PcComponentDTO>
{
    public PcComponentMapper(IMapper mapper) : base(mapper)
    {
    }

    public PcComponentCartDTO MapCart(DAL.DTO.PcComponent.PcComponentCartDTO pcComponent)
    {
        return Mapper.Map<PcComponentCartDTO>(pcComponent);
    }
    
    public PcComponentStoreDTO MapStore(DAL.DTO.PcComponent.PcComponentStoreDTO pcComponent)
    {
        return Mapper.Map<PcComponentStoreDTO>(pcComponent);
    }
}