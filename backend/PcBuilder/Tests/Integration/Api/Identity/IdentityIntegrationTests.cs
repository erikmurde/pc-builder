using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Public.DTO.V1.Identity;

namespace Tests.Integration.Api.Identity;

public class IdentityIntegrationTests : IClassFixture<CustomWebAppFactory<Program>>
{
    private readonly HttpClient _client;

    private readonly JsonSerializerOptions _camelCaseJsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    
    public IdentityIntegrationTests(CustomWebAppFactory<Program> factory)
    {
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });   
    }

    [Fact(DisplayName = "POST - register new user")]
    public async Task TestRegisterNewUser()
    {
        // Arrange
        const string url = "/api/v1/identity/account/register?expiresInSeconds=1";
        const string email = "register@app.com";
        const string password = "Test.1";

        var registerData = new Register
        {
            Email = email,
            Password = password
        };
        var data = JsonContent.Create(registerData);

        // Act
        var response = await _client.PostAsync(url, data);

        // Assert
        Assert.True(response.IsSuccessStatusCode);
        
        var responseContent = await response.Content.ReadAsStringAsync();
        VerifyJwtContent(responseContent, email, DateTime.Now.AddSeconds(2).ToUniversalTime());
    }

    private void VerifyJwtContent(string jwt, string email, DateTime validToIsSmallerThan)
    {
        var jwtResponse = JsonSerializer.Deserialize<JWTResponse>(jwt, _camelCaseJsonSerializerOptions);

        Assert.NotNull(jwtResponse);
        Assert.NotNull(jwtResponse.RefreshToken);
        Assert.NotNull(jwtResponse.JWT);

        // verify the actual JWT
        var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(jwtResponse.JWT);
        
        Assert.Equal(email, jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value);
        Assert.True(jwtToken.ValidTo < validToIsSmallerThan);
    }

    private async Task<string> RegisterNewUser(string email, string password, int expiresInSeconds = 1)
    {
        var url = $"/api/v1/identity/account/register?expiresInSeconds={expiresInSeconds}";

        var registerData = new Register
        {
            Email = email,
            Password = password
        };

        var data = JsonContent.Create(registerData);
        
        // Act
        var response = await _client.PostAsync(url, data);
        var responseContent = await response.Content.ReadAsStringAsync();
        
        // Assert
        Assert.True(response.IsSuccessStatusCode);
        VerifyJwtContent(responseContent, email, DateTime.Now.AddSeconds(expiresInSeconds + 1).ToUniversalTime());

        return responseContent;
    }

    [Fact(DisplayName = "POST - login user")]
    public async Task TestLoginUser()
    {
        const string email = "login@app.com";
        const string password = "Test.1";

        // Arrange
        await RegisterNewUser(email, password);
        
        const string url  = "/api/v1/identity/account/login?expiresInSeconds=1";

        var loginData = new Login
        {
            Email = email,
            Password = password,
        };

        var data = JsonContent.Create(loginData);

        // Act
        var response = await _client.PostAsync(url, data);
        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.True(response.IsSuccessStatusCode);
        VerifyJwtContent(responseContent, email, DateTime.Now.AddSeconds(2).ToUniversalTime());
    }

    [Fact(DisplayName = "POST - JWT expired")]
    public async Task TestJWTExpired()
    {
        const string email = "expired@app.com";
        const string password = "Test.1";
        const int expiresInSeconds = 3;

        const string url = "/api/v1/orders";

        // Arrange
        var jwt = await RegisterNewUser(email, password, expiresInSeconds);
        var jwtResponse = JsonSerializer.Deserialize<JWTResponse>(jwt, _camelCaseJsonSerializerOptions);
        
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse!.JWT);

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.True(response.IsSuccessStatusCode);

        // Arrange
        await Task.Delay((expiresInSeconds + 2) * 1000);

        var request2 = new HttpRequestMessage(HttpMethod.Get, url);
        request2.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse.JWT);

        // Act
        var response2 = await _client.SendAsync(request2);

        // Assert
        Assert.False(response2.IsSuccessStatusCode);
    }

    [Fact(DisplayName = "POST - JWT renewal")]
    public async Task TestJWTRenewal()
    {
        const string email = "renewal@app.com";
        const string password = "Test.1";
        const int expiresInSeconds = 3;

        const string url = "/api/v1/orders";

        // Arrange
        var jwt = await RegisterNewUser(email, password, expiresInSeconds);
        var jwtResponse = JsonSerializer.Deserialize<JWTResponse>(jwt, _camelCaseJsonSerializerOptions);
        
        // let the jwt expire
        await Task.Delay((expiresInSeconds + 2) * 1000);

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse!.JWT);

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.False(response.IsSuccessStatusCode);

        // Arrange
        var refreshUrl = $"/api/v1/identity/account/refreshToken?expiresInSeconds={expiresInSeconds}";
        var refreshData = new JWTResponse
        {
            JWT = jwtResponse.JWT,
            RefreshToken = jwtResponse.RefreshToken,
        };

        var data =  JsonContent.Create(refreshData);
        
        var response2 = await _client.PostAsync(refreshUrl, data);
        var responseContent2 = await response2.Content.ReadAsStringAsync();
        
        Assert.True(response2.IsSuccessStatusCode);
        
        jwtResponse = JsonSerializer.Deserialize<JWTResponse>(responseContent2, _camelCaseJsonSerializerOptions);

        var request3 = new HttpRequestMessage(HttpMethod.Get, url);
        request3.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse!.JWT);

        // Act
        var response3 = await _client.SendAsync(request3);
        // Assert
        Assert.True(response3.IsSuccessStatusCode);
    }
}