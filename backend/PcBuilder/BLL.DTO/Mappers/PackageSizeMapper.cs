using AutoMapper;
using BLL.DTO.PackageSize;
using DAL.Base;

namespace BLL.DTO.Mappers;

public class PackageSizeMapper : BaseMapper<PackageSizeDTO, DAL.DTO.PackageSize.PackageSizeDTO>
{
    public PackageSizeMapper(IMapper mapper) : base(mapper)
    {
    }
}