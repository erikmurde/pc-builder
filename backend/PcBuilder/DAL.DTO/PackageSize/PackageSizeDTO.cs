using Domain.Base;

namespace DAL.DTO.PackageSize;

public class PackageSizeDTO : DomainEntityId
{
    public string SizeName { get; set; } = default!;
}