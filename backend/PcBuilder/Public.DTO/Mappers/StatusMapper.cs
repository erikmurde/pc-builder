using AutoMapper;
using DAL.Base;
using Public.DTO.V1.Status;

namespace Public.DTO.Mappers;

public class StatusMapper : BaseMapper<StatusDTO, BLL.DTO.Status.StatusDTO>
{
    public StatusMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public BLL.DTO.Status.StatusDTO MapCreate(StatusCreateDTO status)
    {
        return Mapper.Map<BLL.DTO.Status.StatusDTO>(status);
    }
}