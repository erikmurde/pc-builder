using System.Net;
using Microsoft.AspNetCore.Identity;
using Public.DTO.V1;

namespace Helpers.Base;

public static class IdentityErrorHelper
{
    public static RestErrorResponse MissingDataError()
    {
        return new RestErrorResponse
        {
            Status = HttpStatusCode.BadRequest,
            ErrorMessage = "Email or password is missing!"
        };
    } 
    public static RestErrorResponse InvalidEmailError(string email)
    {
        return new RestErrorResponse
        {
            Status = HttpStatusCode.BadRequest,
            ErrorMessage = $"{email} is not a valid email address!"
        };
    }
    public static RestErrorResponse UserAlreadyExistsError(string email)
    {
        return new RestErrorResponse
        {
            Status = HttpStatusCode.BadRequest,
            ErrorMessage = $"User with email {email} already exists!"
        };
    }
    
    public static RestErrorResponse UserNotFoundError(string email)
    {
        return new RestErrorResponse
        {
            Status = HttpStatusCode.BadRequest,
            ErrorMessage = $"User with email {email} not found!"
        };
    }
    
    public static RestErrorResponse InvalidSignInError()
    {
        return new RestErrorResponse
        {
            Status = HttpStatusCode.BadRequest,
            ErrorMessage = "Invalid email or password!"
        };
    }
    
    public static RestErrorResponse CannotCreateUserError(IdentityResult result)
    {
        return new RestErrorResponse
        {
            Status = HttpStatusCode.BadRequest,
            ErrorMessage = $"Error creating user: {result}"
        };
    }
    
    public static RestErrorResponse MissingTokenError()
    {
        return new RestErrorResponse
        {
            Status = HttpStatusCode.BadRequest,
            ErrorMessage = "Token is missing!"
        };
    }
    
    public static RestErrorResponse CannotParseTokenError(string errorMessage)
    {
        return new RestErrorResponse
        {
            Status = HttpStatusCode.BadRequest,
            ErrorMessage = $"Cannot parse token: {errorMessage}!"
        };
    }
    
    public static RestErrorResponse InvalidTokenError()
    {
        return new RestErrorResponse
        {
            Status = HttpStatusCode.BadRequest,
            ErrorMessage = "Token is invalid!"
        };
    }
    
    public static RestErrorResponse InvalidTokenDataError()
    {
        return new RestErrorResponse
        {
            Status = HttpStatusCode.BadRequest,
            ErrorMessage = "Token data does not match database!"
        };
    }
    
    public static RestErrorResponse MissingEmailError()
    {
        return new RestErrorResponse
        {
            Status = HttpStatusCode.BadRequest,
            ErrorMessage = "Email not in token!"
        };
    }
    
    public static RestErrorResponse IncorrectTokenAmountError(string email, int validCount)
    {
        return new RestErrorResponse
        {
            Status = HttpStatusCode.BadRequest,
            ErrorMessage = $"User with email {email} has incorrect number of valid tokens: {validCount}"
        };
    }
}