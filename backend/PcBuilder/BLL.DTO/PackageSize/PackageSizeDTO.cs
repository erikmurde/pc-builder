using Domain.Base;

namespace BLL.DTO.PackageSize;

public class PackageSizeDTO : DomainEntityId
{
    public string SizeName { get; set; } = default!;
}