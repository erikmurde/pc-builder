using System.Net;

namespace Public.DTO.V1;

public class RestErrorResponse
{
    public HttpStatusCode Status { get; set; }
    public string ErrorMessage { get; set; } = default!;
}