using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;

namespace Tests.Integration;

public class HomePageTests : IClassFixture<CustomWebAppFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly ITestOutputHelper? _testOutputHelper;

    public HomePageTests(CustomWebAppFactory<Program> factory, ITestOutputHelper testOutputHelper) : this(factory)
    {
        _testOutputHelper = testOutputHelper;
    }
    
    private HomePageTests(CustomWebAppFactory<Program> factory)
    {
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact(DisplayName = "GET - check that home page loads")]
    public async Task DefaultHomePageTest()
    {
        var response = await _client.GetAsync("/");
        
        response.EnsureSuccessStatusCode();
        
       _testOutputHelper?.WriteLine(await response.Content.ReadAsStringAsync());
    }
}