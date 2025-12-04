using AutoMapper;
using DAL.Base;
using Public.DTO.V1.PackageSize;

namespace Public.DTO.Mappers;

public class PackageSizeMapper : BaseMapper<PackageSizeDTO, BLL.DTO.PackageSize.PackageSizeDTO>
{
    public PackageSizeMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public BLL.DTO.PackageSize.PackageSizeDTO MapCreate(PackageSizeCreateDTO size)
    {
        return Mapper.Map<BLL.DTO.PackageSize.PackageSizeDTO>(size);
    }
}