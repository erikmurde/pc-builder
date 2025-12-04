using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO.ComponentAttribute;
using BLL.DTO.Mappers;
using DAL.Contracts.App;
using Helpers.Base;

namespace BLL.App.Services;

public class ComponentAttributeService : 
    BaseEntityService<ComponentAttributeDTO, DAL.DTO.ComponentAttribute.ComponentAttributeDTO, 
        IComponentAttributeRepository>, IComponentAttributeService
{
    private readonly IAppUOW _uow;
    private readonly ComponentAttributeMapper _mapper;

    public ComponentAttributeService(IAppUOW uow, ComponentAttributeMapper mapper) : 
        base(uow.ComponentAttributeRepository, mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<ComponentAttributeEditDTO?> FindAsyncEdit(Guid id)
    {
        var componentAttribute = await _uow.ComponentAttributeRepository.FindAsyncEdit(id);
        return componentAttribute == null ? null : _mapper.MapEdit(componentAttribute);
    }

    public async Task<ComponentAttributeEditDTO> Update(ComponentAttributeEditDTO componentAttribute)
    {
        if (!EntityValidationHelper.ValidateComponentAttribute(componentAttribute))
        {
            throw new ArgumentException("");
        }
        
        var dalComponentAttribute = await _uow.ComponentAttributeRepository.FindAsyncEdit(componentAttribute.Id);
        if (dalComponentAttribute == null)
        {
            throw new ArgumentException("");
        }

        componentAttribute.ComponentId = dalComponentAttribute.ComponentId;
        dalComponentAttribute = _mapper.MapEdit(componentAttribute);

        if (await HasDuplicateAttributes(dalComponentAttribute))
        {
            throw new ArgumentException("Duplicate");
        }

        var result = _uow.ComponentAttributeRepository.Update(dalComponentAttribute);
        
        return _mapper.MapEdit(result);
    }
    
    public async Task<ComponentAttributeEditDTO> Add(ComponentAttributeEditDTO componentAttribute)
    {
        if (!EntityValidationHelper.ValidateComponentAttribute(componentAttribute))
        {
            throw new ArgumentException("");
        }
        
        var dalComponentAttribute = _mapper.MapEdit(componentAttribute);

        if (await HasDuplicateAttributes(dalComponentAttribute))
        {
            throw new ArgumentException("Duplicate");
        }

        var result = _uow.ComponentAttributeRepository.Add(dalComponentAttribute);

        return _mapper.MapEdit(result);
    }
    
    private async Task<bool> HasDuplicateAttributes(DAL.DTO.ComponentAttribute.ComponentAttributeEditDTO componentAttribute)
    {
        // Component should not have multiple of the same attribute
            
        var componentAttributes = 
            (await _uow.ComponentAttributeRepository.AllAsyncEdit(componentAttribute.ComponentId))
            .ToList();

        return componentAttributes.Any(c => 
            c.Id != componentAttribute.Id && // Skip own id when editing
            c.AttributeId == componentAttribute.AttributeId);
    }
}