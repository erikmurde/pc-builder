namespace Public.DTO.V1.Identity;

public class JWTResponse
{
    public string JWT { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}