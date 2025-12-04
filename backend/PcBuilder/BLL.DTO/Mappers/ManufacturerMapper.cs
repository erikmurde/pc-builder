using AutoMapper;
using BLL.DTO.Manufacturer;
using DAL.Base;

namespace BLL.DTO.Mappers;

public class ManufacturerMapper : BaseMapper<ManufacturerDTO, DAL.DTO.Manufacturer.ManufacturerDTO>
{
    public ManufacturerMapper(IMapper mapper) : base(mapper)
    {
    }
}