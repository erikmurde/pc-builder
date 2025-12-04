using System.Net;
using Public.DTO.V1;

namespace Helpers.Base;

public static class EntityErrorHelper
{   
    public static RestErrorResponse InvalidEntityError()
    {
        return new RestErrorResponse
        {
            Status = HttpStatusCode.BadRequest,
            ErrorMessage = "Resource has invalid data!"
        };
    } 
    public static RestErrorResponse CannotFetchEntityError()
    {
        return new RestErrorResponse
        {
            Status = HttpStatusCode.NotFound,
            ErrorMessage = "Resource not in database or belongs to a different user!"
        };
    }

    public static RestErrorResponse EntitiesNotMatchingError()
    {
        return new RestErrorResponse
        {
            Status = HttpStatusCode.BadRequest,
            ErrorMessage = "Resource in database does not match request!"
        };
    }
    
    public static RestErrorResponse CannotDeleteEntityError()
    {
        return new RestErrorResponse
        {
            Status = HttpStatusCode.BadRequest,
            ErrorMessage = "This resource cannot be deleted!"
        };
    }

    public static RestErrorResponse DuplicateAttributeError()
    {
        return new RestErrorResponse
        {
            Status = HttpStatusCode.BadRequest,
            ErrorMessage = "Attribute already exists!"
        };
    }
}