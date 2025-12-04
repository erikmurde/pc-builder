using DAL.Contracts.Base;
using DAL.DTO.ComponentAttribute;
using Domain.App;

namespace DAL.Contracts.App;

public interface IComponentAttributeRepository : 
    IBaseRepository<ComponentAttributeDTO>, IComponentAttributeRepositoryCustom<ComponentAttributeEditDTO>
{
    public Task<IEnumerable<ComponentAttributeDTO>> AllAsync(Guid componentId);
    public Task<IEnumerable<ComponentAttributeEditDTO>> AllAsyncEdit(Guid componentId);
    public Task<IEnumerable<Guid>> AllAsyncAttributeId(Guid componentId);
    public ComponentAttributeEditDTO Update(ComponentAttributeEditDTO componentAttribute);
    public ComponentAttributeEditDTO Add(ComponentAttributeEditDTO componentAttribute);
}

public interface IComponentAttributeRepositoryCustom<TEdit>
{
    public Task<TEdit?> FindAsyncEdit(Guid id);
}