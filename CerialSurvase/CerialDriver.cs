using System.Net;
using Google.Protobuf.WellKnownTypes;

public class CerialDriver
{
    private static readonly string SLNPATH = DotEnv.TryGetSolutionDirectoryInfo().FullName;
    private static void Main(string[] args)
    {
        DotEnv.Load(Path.Combine(SLNPATH, ".env"));
        CerialContext.IsTesting = bool.Parse(Environment.GetEnvironmentVariable("TESTING") ?? "false");
        var tmp = Environment.GetEnvironmentVariables();
        if (!CerialContext.GetInstanse.Cerial.Any())
        {
            Parser.GetInstanse.LoadFromCSV(Path.Combine(SLNPATH, "Data\\Cereal.csv"));
        }
        // return;
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        Configure(app);

        app.Run();
    }
    public static void Configure(IApplicationBuilder app)
    {

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet(
                "/", () => "hello world"
            );
            HttpGetters(endpoints);

            endpoints.MapPut("/updateCerial/{id}", CerialAPI.UpdateCerial);

            endpoints.MapDelete("/deleteCerial/{id}", CerialAPI.Delete);

            endpoints.MapPost("/newcerial", CerialAPI.NewCerialAsync);
        });
    }

    private static void HttpGetters(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/getCerial", CerialAPI.GetAllCerial);
        endpoints.MapGet("/getCerial/{cerialname}", CerialAPI.GetCerial);
        endpoints.MapGet("/getCerial/filter", CerialAPI.FilterdGet);
        endpoints.MapGet("/getCerialImg/{cerialname}", async context => CerialAPI.GetImageForCerial(context, Path.Combine(SLNPATH, "Data")));
    }
}
