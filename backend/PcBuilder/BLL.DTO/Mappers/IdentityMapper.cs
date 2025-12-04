using AutoMapper;
using BLL.DTO.Identity;
using DAL.Base;

namespace BLL.DTO.Mappers;

public class IdentityMapper : BaseMapper<AppRefreshTokenDTO, DAL.DTO.Identity.AppRefreshTokenDTO>
{
    public IdentityMapper(IMapper mapper) : base(mapper)
    {
    }
}