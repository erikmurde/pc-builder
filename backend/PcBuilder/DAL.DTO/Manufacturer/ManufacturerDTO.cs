using Domain.Base;

namespace DAL.DTO.Manufacturer;

public class ManufacturerDTO : DomainEntityId
{
    public string ManufacturerName { get; set; } = default!;
}