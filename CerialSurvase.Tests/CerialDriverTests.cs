using System.Collections;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;

public class CerialDriverTests : IClassFixture<WebApplicationFactory<CerialDriver>>
{
    readonly HttpClient client;
    public CerialDriverTests(WebApplicationFactory<CerialDriver> app)
    {
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
    public async Task GetAllCerials()
    {
        var expected = 77;

        var response = await client.GetAsync("/getcerial");
        var actual = await response.Content.ReadFromJsonAsync<Cerial[]>();

        Assert.NotNull(actual);
        Assert.Equal(expected, actual.Length);
    }

    [Fact]
    public async Task GetOneCerial()
    {
        // Given
        var expected = new Cerial("Kix", "G", "C", 110, 2, 1, 260, 0, 21, 3, 40, 25, 2, 1, 1.5f, 39241114);
        // When
        var response = await client.GetAsync("/getcerial/kix");
        var actual = await response.Content.ReadFromJsonAsync<Cerial>();

        // Then
        Assert.NotNull(actual);
        Assert.Equal(expected.Name, actual.Name);
    }
    [Theory]
    [MemberData(nameof(CerialFilterTestData))]
    public async Task FilterCerial(string filter){
        // TODO:
        throw new NotImplementedException();
    }



    [Theory]
    [MemberData(nameof(CerialUpdateTestData))]
    public async Task UpdateCerial(string name, string mfr, string type, int calories, int protein, int fat, int sodium, float fiber, float carbo, int sugars, int potass, int vitamins, int shelf, float weight, float cups, float rating, int id)
    {
        // Given
        Cerial toUpdate = new(name, mfr, type, calories, protein, fat, sodium, fiber, carbo, sugars, potass, vitamins, shelf, weight, cups, rating) { ID = (int)id };
        var expected = await (await client.GetAsync($"/getcerial/{name}")).Content.ReadFromJsonAsync<Cerial>();
        if (expected == null) return;
        // When
        var response = await client.PutAsJsonAsync($"updatecerial/{id}", toUpdate);

        // Then
        Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
        Assert.Equal(expected.mfr, mfr);
    }

    public static IEnumerable<object[]> CerialFilterTestData =>
        new List<object[]>{
            new object[] {"calories=120"},
            // todo: add more for more robustness
        };
    public static IEnumerable<object[]> CerialUpdateTestData =>
        new List<object[]>{
            new object[] {"Kix", "P", "C", 110, 2, 1, 260, 0, 21, 3, 40, 25, 2, 1, 1.5f, 39241114 , 9},
            // todo: add more for more robustness
        };
    

}
