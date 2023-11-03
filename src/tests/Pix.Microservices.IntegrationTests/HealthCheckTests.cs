using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Pix.Microservices.IntegrationTests;

public class HealthCheckTests : IClassFixture<WebApplicationFactory<Pix.Users.Api.Startup>>
{
    private readonly HttpClient _client;

    public HealthCheckTests(WebApplicationFactory<Pix.Users.Api.Startup> factory)
    {
        _client = factory.WithWebHostBuilder(builder =>
        {
            builder.UseSetting("ConnectionStrings:DatabaseConnection", "Host=localhost;Port=5432;Database=pix_test;User Id=postgres;Password=postgres;");
            builder.UseSetting("UseSqlServer", "false");
        }).CreateClient();
    }

    [Fact]
    public async Task HealthLive_ShouldReturn200()
    {
        var response = await _client.GetAsync("/health/live");
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }
}
