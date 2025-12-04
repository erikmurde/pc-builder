using DTO.Base;

namespace BLL.DTO.ComponentAttribute;

public class ComponentAttributeEditDTO : ComponentAttributeBaseDTO
{
    public Guid Id { get; set; }
    public Guid ComponentId { get; set; }
    public Guid AttributeId { get; set; }
}