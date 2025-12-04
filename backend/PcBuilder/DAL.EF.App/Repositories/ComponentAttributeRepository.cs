using DAL.Contracts.App;
using DAL.DTO.ComponentAttribute;
using DAL.DTO.Mappers;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class ComponentAttributeRepository : 
    EFBaseRepository<ComponentAttributeDTO, ComponentAttribute, ApplicationDbContext>, IComponentAttributeRepository
{
    private readonly ComponentAttributeMapper _mapper;
    
    public ComponentAttributeRepository(ApplicationDbContext dataContext, ComponentAttributeMapper mapper) : 
        base(dataContext, mapper)
    {
        _mapper = mapper;
    }
    
    public override async Task<IEnumerable<ComponentAttributeDTO>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(c => c.Attribute)
            .Select(c => new ComponentAttributeDTO
            {
                Id = c.Id,
                AttributeName = c.Attribute!.AttributeName,
                AttributeValue = c.AttributeValue
            })
            .AsNoTracking()
            .ToListAsync();
    }
    
    public async Task<IEnumerable<ComponentAttributeDTO>> AllAsync(Guid componentId)
    {
        return await RepositoryDbSet
            .Where(c => c.ComponentId == componentId)
            .Include(c => c.Attribute)
            .Select(c => new ComponentAttributeDTO
            {
                Id = c.Id,
                AttributeName = c.Attribute!.AttributeName,
                AttributeValue = c.AttributeValue
            })
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<ComponentAttributeEditDTO>> AllAsyncEdit(Guid componentId)
    {
        return await RepositoryDbSet
            .Where(c => c.ComponentId == componentId)
            .Select(c => new ComponentAttributeEditDTO
            {
                Id = c.Id,
                ComponentId = c.ComponentId,
                AttributeId = c.AttributeId,
                AttributeValue = c.AttributeValue
            })
            .AsNoTracking()
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Guid>> AllAsyncAttributeId(Guid componentId)
    {
        return await RepositoryDbSet
            .Where(c => c.ComponentId == componentId)
            .Select(c => c.AttributeId)
            .ToListAsync();
    }

    public override async Task<ComponentAttributeDTO?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Where(c => c.Id == id)
            .Include(c => c.Attribute)
            .Select(c => new ComponentAttributeDTO
            {
                Id = c.Id,
                AttributeName = c.Attribute!.AttributeName,
                AttributeValue = c.AttributeValue
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }
    
    public async Task<ComponentAttributeEditDTO?> FindAsyncEdit(Guid id)
    {
        return await RepositoryDbSet
            .Where(c => c.Id == id)
            .Select(c => new ComponentAttributeEditDTO
            {
                Id = c.Id,
                ComponentId = c.ComponentId,
                AttributeId = c.AttributeId,
                AttributeValue = c.AttributeValue
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public ComponentAttributeEditDTO Update(ComponentAttributeEditDTO componentAttribute)
    {
        return _mapper.MapEdit(RepositoryDbSet.Update(_mapper.MapEdit(componentAttribute)).Entity);
    }

    public ComponentAttributeEditDTO Add(ComponentAttributeEditDTO componentAttribute)
    {
        return _mapper.MapEdit(RepositoryDbSet.Add(_mapper.MapEdit(componentAttribute)).Entity);
    }
}