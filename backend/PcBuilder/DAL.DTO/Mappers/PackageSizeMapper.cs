using AutoMapper;
using DAL.Base;
using DAL.DTO.PackageSize;

namespace DAL.DTO.Mappers;

public class PackageSizeMapper : BaseMapper<PackageSizeDTO, Domain.App.PackageSize>
{
    public PackageSizeMapper(IMapper mapper) : base(mapper)
    {
    }
}