using AutoMapper;
using DAL.Base;
using Public.DTO.V1.Manufacturer;

namespace Public.DTO.Mappers;

public class ManufacturerMapper : BaseMapper<ManufacturerDTO, BLL.DTO.Manufacturer.ManufacturerDTO>
{
    public ManufacturerMapper(IMapper mapper) : base(mapper)
    {
    }

    public BLL.DTO.Manufacturer.ManufacturerDTO MapCreate(ManufacturerCreateDTO manufacturer)
    {
        return Mapper.Map<BLL.DTO.Manufacturer.ManufacturerDTO>(manufacturer);
    }
}