using Domain.Base;

namespace BLL.DTO.Identity;

public class AppRefreshTokenDTO : BaseRefreshToken
{
    public Guid AppUserId { get; set; }
}