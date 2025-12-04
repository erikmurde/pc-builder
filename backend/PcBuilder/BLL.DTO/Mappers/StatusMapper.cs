using AutoMapper;
using BLL.DTO.Status;
using DAL.Base;

namespace BLL.DTO.Mappers;

public class StatusMapper : BaseMapper<StatusDTO, DAL.DTO.Status.StatusDTO>
{
    public StatusMapper(IMapper mapper) : base(mapper)
    {
    }
}