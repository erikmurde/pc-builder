using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO.Status;
using BLL.DTO.Mappers;
using DAL.Contracts.App;
using Helpers.Base;

namespace BLL.App.Services;

public class StatusService : 
    BaseEntityService<StatusDTO, DAL.DTO.Status.StatusDTO, IStatusRepository>, IStatusService
{
    private readonly IAppUOW _uow;
    private readonly StatusMapper _mapper;

    public StatusService(IAppUOW uow, StatusMapper mapper) : 
        base(uow.StatusRepository, mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public override StatusDTO Update(StatusDTO status)
    {
        if (!EntityValidationHelper.ValidateStatus(status))
        {
            throw new ArgumentException("");
        }

        var result = _uow.StatusRepository.Update(_mapper.Map(status));
        return _mapper.Map(result);
    }

    public override StatusDTO Add(StatusDTO status)
    {
        if (!EntityValidationHelper.ValidateStatus(status))
        {
            throw new ArgumentException("");
        }

        var result = _uow.StatusRepository.Add(_mapper.Map(status));
        return _mapper.Map(result);
    }
}