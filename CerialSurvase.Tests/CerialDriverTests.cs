using System.Collections;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;

public class CerialDriverTests : IClassFixture<WebApplicationFactory<CerialDriver>>, IDisposable
{
    readonly HttpClient client;
    public CerialDriverTests(WebApplicationFactory<CerialDriver> app)
    {
        Environment.SetEnvironmentVariable("TESTING", "true");
        client = app.CreateClient();
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
        var expected = new Cerial("Life","Q","C",100,4,2,150,2,12,6,95,25,2,1,0.67f,45328074);
        // When
        var response = await client.GetAsync("/getcerial/life");
        var actual = await response.Content.ReadFromJsonAsync<Cerial>();

        // Then
        Assert.NotNull(actual);
        Assert.Equal(expected.Name, actual.Name);
    }

    [Theory]
    [MemberData(nameof(CerialFilterTestData))]
    public async Task FilterCerial(string filter, int expectedAmount)
    {

        var response = await client.GetAsync($"/getCerial/filter?{filter}");
        var actual = await response.Content.ReadFromJsonAsync<List<Cerial>>();
        if (expectedAmount != 0)
        {
            Assert.NotNull(actual);
        }
        Assert.Equal(expectedAmount, actual!.Count);
    }
    public static IEnumerable<object[]> CerialFilterTestData =>
        new List<object[]>{
            new object[] {"calories=70", 2},
            // todo: add more for more robustness
        };

    // Sometimes works sometimes dont. Dont know why
    [Theory]
    [MemberData(nameof(CerialUpdateTestData))]
    public async Task UpdateCerial(string name, string mfr, string type, int calories, int protein, int fat, int sodium, float fiber, float carbo, int sugars, int potass, int vitamins, int shelf, float weight, float cups, float rating, int id)
    {
        // Given
        Cerial toUpdate = new(name, mfr, type, calories, protein, fat, sodium, fiber, carbo, sugars, potass, vitamins, shelf, weight, cups, rating) { ID = (int)id };
        var expected = await (await client.GetAsync($"/getcerial/{name}")).Content.ReadFromJsonAsync<Cerial>();
        if (expected == null) return;
        // When
        var response = await client.PutAsJsonAsync($"/updatecerial/{id}", toUpdate);

        // Then

        var actual = await response.Content.ReadFromJsonAsync<Cerial>();
        Assert.NotNull(actual);
        Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
        Assert.Equal(mfr, actual.mfr);
    }

    public static IEnumerable<object[]> CerialUpdateTestData =>
        new List<object[]>{
            new object[] {"Kix", "P", "C", 110, 2, 1, 260, 0, 21, 3, 40, 25, 2, 1, 1.5f, 39241114 , 9},
            // todo: add more for more robustness
        };

    [Fact]
    public async Task Delete()
    {
        var expected = 76;

        await client.DeleteAsync("/deletecerial/1");

        var response = await client.GetAsync("/getcerial");
        var actual = await response.Content.ReadFromJsonAsync<Cerial[]>();

        Assert.NotNull(actual);
        Assert.Equal(expected, actual.Length);

    }

    [Fact]
    public async Task NewCerialAsync()
    {
        // Given
        var newCerial = new Cerial("Name", "P", "C", 110, 2, 1, 260, 0, 21, 3, 40, 25, 2, 1, 1.5f, 30344314);
        // When
        var response = await client.PostAsJsonAsync("/newcerial", newCerial);
        var actual = await (await client.GetAsync($"/getcerial/{newCerial.Name}"))
            .Content.ReadFromJsonAsync<Cerial>();

        // Then
        Assert.NotNull(actual);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(newCerial.Name, actual.Name);
    }

    [Fact]
    public async Task NewCerialWithId()
    {
        // Given
        var newCerial = new Cerial("Name", "P", "C", 110, 2, 1, 260, 0, 21, 3, 40, 25, 2, 1, 1.5f, 30344314) { ID = 9 };
        // When
        var response = await client.PostAsJsonAsync("/newcerial", newCerial);
        var actual = await (await client.GetAsync($"/getcerial/{newCerial.Name}"))
            .Content.ReadFromJsonAsync<Cerial>();

        // Then
        Assert.NotNull(actual);
        Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
        Assert.Equal(newCerial.Name, actual.Name);

    }



    public void Dispose() => CerialContext.Reset();
}
