using BLL.DTO.ComponentAttribute;
using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IComponentAttributeService : 
    IBaseRepository<ComponentAttributeDTO>, IComponentAttributeRepositoryCustom<ComponentAttributeEditDTO>
{
    public Task<ComponentAttributeEditDTO> Update(ComponentAttributeEditDTO componentAttribute);
    public Task<ComponentAttributeEditDTO> Add(ComponentAttributeEditDTO componentAttribute);
}