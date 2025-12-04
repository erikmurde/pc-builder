using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;
using System.Security.Claims;
using Asp.Versioning;
using BLL.Contracts.App;
using BLL.DTO.Identity;
using Domain.App.Identity;
using Helpers.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Public.DTO.V1;
using Public.DTO.V1.Identity;

namespace WebApp.Api.V1.Identity;

/// <summary>
/// Controller for managing user account
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/identity/[controller]/[action]")]
public class AccountController : ControllerBase
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IAppBLL _bll;

    /// <summary>
    /// Constructor for account controller
    /// </summary>
    /// <param name="signInManager">Sign in manager</param>
    /// <param name="userManager">User manager</param>
    /// <param name="configuration">App settings configuration</param>
    /// <param name="bll">Wrapper for services</param>
    public AccountController(
        SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, 
        IConfiguration configuration, IAppBLL bll)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _configuration = configuration;
        _bll = bll;
    }

    /// <summary>
    /// Register new user to the system
    /// </summary>
    /// <param name="registrationData">User information</param>
    /// <param name="expiresInSeconds">Query parameter that sets the jwt expiration time</param>
    /// <returns>JWTResponse with jwt and refresh token</returns>
    [HttpPost]
    [Produces(contentType: MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(JWTResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JWTResponse>> Register(Register registrationData, 
        [FromQuery] int expiresInSeconds)
    {
        if (registrationData.Email == "" || registrationData.Password == "")
        {
            return BadRequest(IdentityErrorHelper.MissingDataError());
        }
        if (!IdentityHelper.EmailIsValid(registrationData.Email))
        {
            return BadRequest(IdentityErrorHelper.InvalidEmailError(registrationData.Email));
        }

        var appUser = await _userManager.FindByEmailAsync(registrationData.Email);
        if (appUser != null)
        {
            return BadRequest(IdentityErrorHelper.UserAlreadyExistsError(registrationData.Email));
        }

        appUser = new ApplicationUser
        {
            Email = registrationData.Email,
            UserName = registrationData.Email,
        };

        var result = await _userManager.CreateAsync(appUser, registrationData.Password);
        if (!result.Succeeded)
        {
            return BadRequest(IdentityErrorHelper.CannotCreateUserError(result));
        }

        var res = await CreateJwt(appUser, 
            expiresInSeconds: expiresInSeconds <= 0 ? null : expiresInSeconds);

        return Ok(res);
    }
    
    /// <summary>
    /// Login user to the system
    /// </summary>
    /// <param name="loginData">User information</param>
    /// <param name="expiresInSeconds">Query parameter that sets the jwt expiration time</param>
    /// <returns>JWTResponse with jwt and refresh token</returns>
    [HttpPost]
    [Produces(contentType: MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(JWTResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JWTResponse>> Login(Login loginData,
        [FromQuery] int expiresInSeconds)
    {
        if (loginData.Email == "" || loginData.Password == "")
        {
            return BadRequest(IdentityErrorHelper.MissingDataError());
        }
        if (!IdentityHelper.EmailIsValid(loginData.Email))
        {
            return BadRequest(IdentityErrorHelper.InvalidEmailError(loginData.Email));
        }
        
        var appUser = await _userManager.FindByEmailAsync(loginData.Email);
        if (appUser == null)
        {
            return BadRequest(IdentityErrorHelper.UserNotFoundError(loginData.Email));
        }

        var result = await _signInManager.CheckPasswordSignInAsync(appUser, loginData.Password, false);
        if (!result.Succeeded)
        {
            return BadRequest(IdentityErrorHelper.InvalidSignInError());
        }

        var userTokens = await _bll.IdentityService.AllAsync(appUser.Id);

        foreach (var userRefreshToken in userTokens)
        {
            if (userRefreshToken.ExpirationTime < DateTime.UtcNow && (
                    userRefreshToken.PreviousExpirationTime == null || 
                    userRefreshToken.PreviousExpirationTime < DateTime.UtcNow))
            {
                _bll.IdentityService.Remove(userRefreshToken);
            }
        }
        
        var res = await CreateJwt(appUser, 
            expiresInSeconds: expiresInSeconds <= 0 ? null : expiresInSeconds);

        return Ok(res);
    }
    
    /// <summary>
    /// Create new jwt for user
    /// </summary>
    /// <param name="refreshTokenModel">User refresh token</param>
    /// <returns>JWTResponse with jwt and refresh token</returns>
    [HttpPost]
    [Produces(contentType: MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(JWTResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JWTResponse>> RefreshToken(RefreshTokenModel refreshTokenModel)
    {
        JwtSecurityToken jwtToken;
        try
        {
            jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(refreshTokenModel.JWT);
            if (jwtToken == null)
            {
                return BadRequest(IdentityErrorHelper.MissingTokenError());
            }
        }
        catch (Exception e)
        {
            return BadRequest(IdentityErrorHelper.CannotParseTokenError(e.Message));
        }
        
        if (!TokenIsValid(refreshTokenModel.JWT))
        {
            return BadRequest(IdentityErrorHelper.InvalidTokenError());
        }
        
        var userEmail = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        if (userEmail == null)
        {
            return BadRequest(IdentityErrorHelper.MissingEmailError());
        }

        var appUser = await _userManager.FindByEmailAsync(userEmail);
        if (appUser == null)
        {
            return BadRequest(IdentityErrorHelper.UserNotFoundError(userEmail));
        }

        var validTokens = (await _bll.IdentityService
                .AllValidAsync(refreshTokenModel.RefreshToken))
                .ToList();
        
        if (validTokens is not { Count: 1 })
        {
            return BadRequest(IdentityErrorHelper.IncorrectTokenAmountError(userEmail, validTokens.Count));
        }

        var refreshToken = validTokens.First();
        await RenewToken(refreshToken, refreshTokenModel.RefreshToken);
        
        var res = await CreateJwt(appUser, refreshToken);

        return Ok(res);
    }

    /// <summary>
    /// Logout user from the system
    /// </summary>
    /// <param name="logout">User refresh token</param>
    /// <returns>LogoutResponse with refresh token delete count</returns>
    [HttpPost]
    [Produces(contentType: MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(LogoutResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RestErrorResponse), StatusCodes.Status400BadRequest)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<LogoutResponse>> Logout(Logout logout)
    {
        var appUser = await _userManager.FindByIdAsync(User.GetUserId().ToString());
        if (appUser == null)
        {
            return BadRequest(IdentityErrorHelper.InvalidTokenDataError());
        }

        var userTokens = await _bll.IdentityService.AllAsync(appUser.Id);
        foreach (var appRefreshToken in userTokens)
        {
            if (appRefreshToken.RefreshToken == logout.RefreshToken ||
                appRefreshToken.PreviousRefreshToken == logout.RefreshToken)
            {
                _bll.IdentityService.Remove(appRefreshToken);   
            }
        }

        var deleteCount = await _bll.SaveChangesAsync();

        return new LogoutResponse { TokenDeleteCount = deleteCount };
    }

    private async Task<JWTResponse> CreateJwt(
        ApplicationUser appUser, AppRefreshTokenDTO? refreshToken = null, int? expiresInSeconds = null)
    {
        if (refreshToken == null)
        {
            refreshToken = new AppRefreshTokenDTO
            {
                AppUserId = appUser.Id
            };
            _bll.IdentityService.Add(refreshToken);
            await _bll.SaveChangesAsync();
        }
        
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);

        var jwt = IdentityHelper.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration.GetValue<string>("JWT:Key")!,
            _configuration.GetValue<string>("JWT:Issuer")!,
            _configuration.GetValue<string>("JWT:Audience")!,
            expiresInSeconds ?? _configuration.GetValue<int>("JWT:ExpiresInSeconds")
        );

        var res = new JWTResponse
        {
            JWT = jwt,
            RefreshToken = refreshToken.RefreshToken
        };

        return res;
    }
    
    private async Task RenewToken(AppRefreshTokenDTO refreshToken, string modelRefreshToken)
    {
        if (refreshToken.RefreshToken == modelRefreshToken)
        {
            refreshToken.PreviousRefreshToken = refreshToken.RefreshToken;
            refreshToken.PreviousExpirationTime = DateTime.UtcNow.AddMinutes(1);

            refreshToken.RefreshToken = Guid.NewGuid().ToString();
            refreshToken.ExpirationTime = DateTime.UtcNow.AddDays(7);

            _bll.IdentityService.Update(refreshToken);
            await _bll.SaveChangesAsync();
        }
    }

    private bool TokenIsValid(string jwt)
    {
        return IdentityHelper.ValidateToken(
            jwt,
            _configuration["JWT:Key"]!,
            _configuration["JWT:Issuer"]!,
            _configuration["JWT:Audience"]!);
    }
}