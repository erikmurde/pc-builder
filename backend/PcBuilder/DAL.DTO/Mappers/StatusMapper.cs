using AutoMapper;
using DAL.Base;
using DAL.DTO.Status;

namespace DAL.DTO.Mappers;

public class StatusMapper : BaseMapper<StatusDTO, Domain.App.Status>
{
    public StatusMapper(IMapper mapper) : base(mapper)
    {
    }
}