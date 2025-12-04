using DAL.Contracts.Base;
using DAL.DTO.Component;

namespace DAL.Contracts.App;

public interface IComponentRepository : 
    IBaseRepository<ComponentDTO>, 
    IComponentRepositoryCustom<ComponentDetailsDTO, ComponentSimpleDTO, ComponentCreateDTO, ComponentEditDTO>
{
    public Task<ComponentSimpleDTO?> FindAsyncSimple(Guid id);
    public Task<ComponentEditDTO> UpdateStock(ComponentEditDTO component);
}

public interface IComponentRepositoryCustom<TDetails, TSimple, TCreate, TEdit>
{
    public Task<IEnumerable<TSimple>> AllAsyncSimple();
    public Task<IEnumerable<TDetails>> AllAsyncMotherboard();
    public Task<TDetails?> FindAsyncDetails(Guid id);
    public Task<TEdit?> FindAsyncEdit(Guid id);
    public TEdit Update(TEdit component);
    public TCreate Add(TCreate component);
}