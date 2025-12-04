using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO.Attribute;
using BLL.DTO.Mappers;
using DAL.Contracts.App;
using Helpers.Base;

namespace BLL.App.Services;

public class AttributeService : 
    BaseEntityService<AttributeDTO, DAL.DTO.Attribute.AttributeDTO, IAttributeRepository>, IAttributeService
{
    private readonly IAppUOW _uow;
    private readonly AttributeMapper _mapper;
    
    public AttributeService(IAppUOW uow, AttributeMapper mapper) : 
        base(uow.AttributeRepository, mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public override AttributeDTO Update(AttributeDTO attribute)
    {
        if (!EntityValidationHelper.ValidateAttribute(attribute))
        {
            throw new ArgumentException("");
        }

        var result = _uow.AttributeRepository.Update(_mapper.Map(attribute));
        return _mapper.Map(result);
    }

    public override AttributeDTO Add(AttributeDTO attribute)
    {
        if (!EntityValidationHelper.ValidateAttribute(attribute))
        {
            throw new ArgumentException("");
        }

        var result = _uow.AttributeRepository.Add(_mapper.Map(attribute));
        return _mapper.Map(result);
    }
}