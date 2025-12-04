using AutoMapper;
using DAL.Base;
using DAL.DTO.Identity;
using Domain.App.Identity;

namespace DAL.DTO.Mappers;

public class IdentityMapper : BaseMapper<AppRefreshTokenDTO, AppRefreshToken>
{
    public IdentityMapper(IMapper mapper) : base(mapper)
    {
    }
}