using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;

public class UnitTest1
{
    [Fact]
    public async Task Test1()
    {
        await using var app = new WebApplicationFactory<CerialDriver>();
        using var client = app.CreateClient();

        var response = await client.GetAsync("/");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
