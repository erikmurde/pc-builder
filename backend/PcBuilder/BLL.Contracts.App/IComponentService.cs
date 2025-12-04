using BLL.DTO.Component;
using BLL.DTO.PcBuild;
using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IComponentService : 
    IBaseRepository<ComponentDTO>, 
    IComponentRepositoryCustom<ComponentDetailsDTO, ComponentSimpleDTO, ComponentCreateDTO, ComponentEditDTO>
{
    public Task<IEnumerable<ComponentDetailsDTO>> AllAsyncSelected(PcBuildEditDTO values);
}