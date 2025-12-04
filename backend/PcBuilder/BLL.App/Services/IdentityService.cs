using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO.Identity;
using BLL.DTO.Mappers;
using Contracts.Base;
using DAL.Contracts.App;

namespace BLL.App.Services;

public class IdentityService : 
    BaseEntityService<AppRefreshTokenDTO, DAL.DTO.Identity.AppRefreshTokenDTO, IAppRefreshTokenRepository>, IIdentityService
{
    private readonly IAppUOW _uow;
    private readonly IMapper<AppRefreshTokenDTO, DAL.DTO.Identity.AppRefreshTokenDTO> _mapper;

    public IdentityService(IAppUOW uow, IdentityMapper mapper) : 
        base(uow.AppRefreshTokenRepository, mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AppRefreshTokenDTO>> AllAsync(Guid userId)
    {
        return (await _uow.AppRefreshTokenRepository.AllAsync(userId))
            .Select(a => _mapper.Map(a));
    }

    public async Task<IEnumerable<AppRefreshTokenDTO>> AllValidAsync(string refreshToken)
    {
        return (await _uow.AppRefreshTokenRepository.AllValidAsync(refreshToken))
            .Select(a => _mapper.Map(a));
    }
}