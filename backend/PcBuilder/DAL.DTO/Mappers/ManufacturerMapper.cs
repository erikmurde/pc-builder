using AutoMapper;
using DAL.Base;
using DAL.DTO.Manufacturer;

namespace DAL.DTO.Mappers;

public class ManufacturerMapper : BaseMapper<ManufacturerDTO, Domain.App.Manufacturer>
{
    public ManufacturerMapper(IMapper mapper) : base(mapper)
    {
    }
}