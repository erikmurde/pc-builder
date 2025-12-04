using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO.Manufacturer;
using BLL.DTO.Mappers;
using DAL.Contracts.App;
using Helpers.Base;

namespace BLL.App.Services;

public class ManufacturerService : 
    BaseEntityService<ManufacturerDTO, DAL.DTO.Manufacturer.ManufacturerDTO, IManufacturerRepository>, IManufacturerService
{
    private readonly IAppUOW _uow;
    private readonly ManufacturerMapper _mapper;
    
    public ManufacturerService(IAppUOW uow, ManufacturerMapper mapper) : 
        base(uow.ManufacturerRepository, mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public override ManufacturerDTO Update(ManufacturerDTO manufacturer)
    {
        if (!EntityValidationHelper.ValidateManufacturer(manufacturer))
        {
            throw new ArgumentException("");
        }

        var result = _uow.ManufacturerRepository.Update(_mapper.Map(manufacturer));
        return _mapper.Map(result);
    }

    public override ManufacturerDTO Add(ManufacturerDTO manufacturer)
    {
        if (!EntityValidationHelper.ValidateManufacturer(manufacturer))
        {
            throw new ArgumentException("");
        }

        var result = _uow.ManufacturerRepository.Add(_mapper.Map(manufacturer));
        return _mapper.Map(result);
    }
}