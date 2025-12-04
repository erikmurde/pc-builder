using Domain.Base;

namespace BLL.DTO.Manufacturer;

public class ManufacturerDTO : DomainEntityId
{
    public string ManufacturerName { get; set; } = default!;
}