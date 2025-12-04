using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using DAL.EF.App.Seeding.Names;
using Microsoft.AspNetCore.Mvc.Testing;
using Public.DTO.V1.CartPc;
using Public.DTO.V1.Discount;
using Public.DTO.V1.Identity;
using Public.DTO.V1.Order;
using Public.DTO.V1.OrderPC;
using Public.DTO.V1.OrderShippingCost;
using Public.DTO.V1.PackageSize;
using Public.DTO.V1.Payment;
using Public.DTO.V1.PcBuild;
using Public.DTO.V1.ShippingCost;
using Public.DTO.V1.ShippingMethod;

namespace Tests.Integration.Api;

public class HappyFlowTests : IClassFixture<CustomWebAppFactory<Program>>
{
    private readonly HttpClient _client;
    
    private readonly JsonSerializerOptions _camelCaseJsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    
    public HappyFlowTests(CustomWebAppFactory<Program> factory)
    {
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact(DisplayName = "GET - get a list of PC builds")]
    public async Task TestGetPcBuilds()
    {
        await GetPcBuilds();
    }
    
    [Fact(DisplayName = "GET - get a PC build that is a prebuilt and is in stock")]
    public async Task TestSelectInStockPrebuiltPcBuild()
    {
        await GetPcBuild();
    }
    
    [Fact(DisplayName = "POST - add a PC build to cart")]
    public async Task TestAddPcBuildToCart()
    {
        const string email = "cart@app.com";
        const string password = "Test.1";
        
        var jwtResponse = await RegisterNewUser(email, password);
        var jwtData = JsonSerializer.Deserialize<JWTResponse>(jwtResponse, _camelCaseJsonSerializerOptions);
        
        Assert.NotNull(jwtData);
        
        await AddPcToCart(jwtData);
    }
    
    [Fact(DisplayName = "POST - place order")]
    public async Task TestPlaceOrder()
    {
        const string email = "order@app.com";
        const string password = "Test.1";
        
        var jwtResponse = await RegisterNewUser(email, password);
        var jwtData = JsonSerializer.Deserialize<JWTResponse>(jwtResponse, _camelCaseJsonSerializerOptions);
     
        Assert.NotNull(jwtData);

        await PlaceOrder(jwtData);
    }

    [Fact(DisplayName = "POST - logout user")]
    public async Task TestLogout()
    {
        const string email = "logout@app.com";
        const string password = "Test.1";
        
        const string url = "api/v1.0/identity/account/logout";
        const string refreshUrl = "api/v1.0/identity/account/RefreshToken";
    
        var jwtResponse = await RegisterNewUser(email, password, 1);
        var jwtData = JsonSerializer.Deserialize<JWTResponse>(jwtResponse, _camelCaseJsonSerializerOptions);

        Assert.NotNull(jwtData);

        var logoutData = new Logout
        {
            RefreshToken = jwtData.RefreshToken
        };
        
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtData.JWT);
        request.Content = JsonContent.Create(logoutData);

        var response = await _client.SendAsync(request);
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.True(response.IsSuccessStatusCode);

        var logout = JsonSerializer.Deserialize<LogoutResponse>(responseContent, _camelCaseJsonSerializerOptions);
        
        Assert.NotNull(logout);
        Assert.Equal(1, logout.TokenDeleteCount);
        
        await Task.Delay(2000);

        var refreshResponse = await _client.PostAsync(refreshUrl, JsonContent.Create(jwtData));

        Assert.False(refreshResponse.IsSuccessStatusCode);
    }

    private async Task<string> RegisterNewUser(string email, string password, int expiresInSeconds = 3600)
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

    private async Task<List<PcBuildDTO>?> GetPcBuilds()
    {
        var response = await _client.GetAsync("api/v1.0/PcBuilds");
        var responseContent = await response.Content.ReadAsStringAsync();
        
        Assert.True(response.IsSuccessStatusCode);
        
        var pcBuilds = JsonSerializer.Deserialize<List<PcBuildDTO>>(responseContent, _camelCaseJsonSerializerOptions);
        
        Assert.NotNull(pcBuilds);
        Assert.IsType<List<PcBuildDTO>>(pcBuilds);
        Assert.NotEmpty(pcBuilds);

        return pcBuilds;
    }

    private async Task<PcBuildDetailsDTO?> GetPcBuild()
    {
        var pcBuilds = await GetPcBuilds();
        
        var pcBuild = pcBuilds!.FirstOrDefault(p => 
            p is { CategoryName: CategoryNames.PrebuiltPc, Stock: > 0 });

        Assert.NotNull(pcBuild);

        var response = await _client.GetAsync($"api/v1.0/PcBuilds/{pcBuild.Id}");
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.True(response.IsSuccessStatusCode);

        var detailedPcBuild = JsonSerializer.Deserialize<PcBuildDetailsDTO>(responseContent, 
            _camelCaseJsonSerializerOptions);
        
        Assert.NotNull(detailedPcBuild);
        Assert.IsType<PcBuildDetailsDTO>(detailedPcBuild);

        Assert.True(detailedPcBuild.Stock > 0);
        Assert.True(detailedPcBuild.PcComponents.Count is 9 or 10);

        return detailedPcBuild;
    }
    
    private async Task<CartPcEditDTO> AddPcToCart(JWTResponse jwtData)
    {
        const string url = "api/v1.0/CartPcs";
        
        var pcBuild = await GetPcBuild();

        var dto = new CartPcCreateDTO
        {
            PcBuildId = pcBuild!.Id,
            Qty = 1
        };
        
        var createRequest = new HttpRequestMessage(HttpMethod.Post, url);
        createRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtData.JWT);
        createRequest.Content = JsonContent.Create(dto);
        
        var createResponse = await _client.SendAsync(createRequest);
        var createResponseContent = await createResponse.Content.ReadAsStringAsync();
        
        Assert.True(createResponse.IsSuccessStatusCode);

        var cartPc = JsonSerializer.Deserialize<CartPcEditDTO>(createResponseContent, _camelCaseJsonSerializerOptions);
        
        Assert.NotNull(cartPc);
        Assert.Equal(cartPc.PcBuildId, pcBuild.Id);

        var cartPcRequest = new HttpRequestMessage(HttpMethod.Get, url);
        cartPcRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtData.JWT);
        
        var cartPcResponse = await _client.SendAsync(cartPcRequest);
        var cartPcResponseContent = await cartPcResponse.Content.ReadAsStringAsync();
        
        Assert.True(cartPcResponse.IsSuccessStatusCode);

        var cartPcs = JsonSerializer.Deserialize<List<CartPcDTO>>(cartPcResponseContent, 
            _camelCaseJsonSerializerOptions);
        
        Assert.NotNull(cartPcs);
        Assert.NotEmpty(cartPcs);
        
        Assert.Contains(cartPcs, c => c.Id == cartPc.Id);

        return cartPc;
    }
    
    private async Task PlaceOrder(JWTResponse jwtData)
    {
        const string url = "api/v1.0/Orders";
    
        var cartPc = await AddPcToCart(jwtData);

        var dto = await GetOrderDto(cartPc, jwtData);

        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtData.JWT);
        request.Content = JsonContent.Create(dto);

        var response = await _client.SendAsync(request);
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.True(response.IsSuccessStatusCode);
        
        var order = JsonSerializer.Deserialize<OrderCreateDTO>(responseContent, _camelCaseJsonSerializerOptions);
        
        Assert.NotNull(order);
        Assert.Equal(order.OrderNr, dto.OrderNr);

        var orderRequest = new HttpRequestMessage(HttpMethod.Get, url);
        orderRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtData.JWT);

        var orderResponse = await _client.SendAsync(orderRequest);
        var orderResponseContent = await orderResponse.Content.ReadAsStringAsync();
        
        Assert.True(orderResponse.IsSuccessStatusCode);
        
        var orders = JsonSerializer.Deserialize<List<OrderDTO>>(orderResponseContent, 
            _camelCaseJsonSerializerOptions);
        
        Assert.NotNull(orders);
        Assert.NotEmpty(orders);
        
        Assert.Contains(orders, o => o.OrderNr == order.OrderNr);
    }
    
    private async Task<OrderCreateDTO> GetOrderDto(CartPcEditDTO cartPc, JWTResponse jwtData)
    {
        const string testString = "Test";
        const decimal testDecimal = 100m;
        
        var discountId = (await GetEntityId<DiscountDTO>("api/v1.0/Discounts")).Id;
        var statusId = (await GetEntityId<DiscountDTO>("api/v1.0/Statuses")).Id;
        var shippingMethodId = (await GetEntityId<ShippingMethodDTO>("api/v1.0/ShippingMethods")).Id;
        var packageSizeId = (await GetEntityId<PackageSizeDTO>("api/v1.0/PackageSizes")).Id;
        var shippingCostId = await GetShippingCostId(jwtData);

        return new OrderCreateDTO
        {
            DiscountId = discountId,
            StatusId = statusId,
            ShippingMethodId = shippingMethodId,
            OrderNr = Guid.NewGuid().ToString()[..12],
            CustomerName = testString,
            CustomerPhoneNumber = testString,
            ShippingAddress = testString,
            ShippingPostalCode = testString,
            OrderPcData = new List<OrderPcCreateDTO>
            {
                new() {
                    PcBuildId = cartPc.PcBuildId,
                    PackageSizeId = packageSizeId,
                    PricePerUnit = testDecimal,
                    Qty = 1
                }
            },
            PaymentData = new List<PaymentCreateDTO>
            {
                new() {
                    PaymentNr = testString,
                    AmountPaid = testDecimal
                }
            },
            OrderShippingCostData = new List<OrderShippingCostCreateDTO>
            {
                new() {
                    ShippingCostId = shippingCostId,
                    TotalCost = testDecimal
                }
            }
        };
    }

    private async Task<Guid> GetShippingCostId(JWTResponse jwtData)
    {
        const string url = "api/v1.0/ShippingCosts";
        
        var packageSize = await GetEntityId<PackageSizeDTO>("api/v1.0/PackageSizes");
        var shippingMethod = await GetEntityId<ShippingMethodDTO>("api/v1.0/ShippingMethods");

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtData.JWT);
        
        var shippingCostResponse = await _client.SendAsync(request);
        var shippingCostResponseContent = await shippingCostResponse.Content.ReadAsStringAsync();
        
        Assert.True(shippingCostResponse.IsSuccessStatusCode);

        var shippingCosts = JsonSerializer.Deserialize<List<ShippingCostDTO>>(shippingCostResponseContent, 
            _camelCaseJsonSerializerOptions);
        
        Assert.NotNull(shippingCosts);
        Assert.NotEmpty(shippingCosts);

        var shippingCost = shippingCosts.FirstOrDefault(s =>
            s.PackageSize == packageSize.SizeName && 
            s.ShippingMethod == shippingMethod.MethodName);
        
        Assert.NotNull(shippingCost);

        return shippingCost.Id;
    }

    private async Task<TEntity> GetEntityId<TEntity>(string url)
    {
        var response = await _client.GetAsync(url);
        var responseContent = await response.Content.ReadAsStringAsync();
        
        Assert.True(response.IsSuccessStatusCode);

        var entities = JsonSerializer.Deserialize<List<TEntity>>(responseContent, _camelCaseJsonSerializerOptions);
        
        Assert.NotNull(entities);
        Assert.NotEmpty(entities);
        
        return entities.First();
    }
}