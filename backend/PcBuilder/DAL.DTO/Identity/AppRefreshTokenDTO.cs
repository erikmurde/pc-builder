using Domain.Base;

namespace DAL.DTO.Identity;

public class AppRefreshTokenDTO : BaseRefreshToken
{
    public Guid AppUserId { get; set; }
}