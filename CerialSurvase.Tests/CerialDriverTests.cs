using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;

public class CerialDriverTests: IClassFixture<WebApplicationFactory<CerialDriver>>
{
    readonly HttpClient client;
    public CerialDriverTests(WebApplicationFactory<CerialDriver> app){
        client = app.CreateClient();
        Environment.SetEnvironmentVariable("TESTING", "true");
    }
    [Fact]
    public async Task HttpConnection()
    {
        var response = await client.GetAsync("/");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetAllCerials(){
        var expected = 77;

        var response = await client.GetAsync("/getcerial");
        var actual = await response.Content.ReadFromJsonAsync<Cerial[]>();
        
        Assert.NotNull(actual);
        Assert.Equal(expected,actual.Length);
    }
    [Fact]
    public async Task GetOneCerial()
    {
        // Given
        var expected = new Cerial("Kix","G","C",110,2,1,260,0,21,3,40,25,2,1,1.5f,39241114);
        // When
        var response = await client.GetAsync("/getcerial/kix");
        var actual = await response.Content.ReadFromJsonAsync<Cerial>();
        // Then
        
    }
}
