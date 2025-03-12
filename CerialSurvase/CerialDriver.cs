using System.Net;
using Google.Protobuf.WellKnownTypes;

public class CerialDriver
{
    private static readonly string SLNPATH = DotEnv.TryGetSolutionDirectoryInfo().FullName;
    private static void Main(string[] args)
    {
        DotEnv.Load(Path.Combine(SLNPATH, ".env"));
        CerialContext.IsTesting = bool.Parse(Environment.GetEnvironmentVariable("TESTING") ?? "false");
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
            endpoints.MapGet("/getCerial", async (context) =>
            {
                await Task.Run(() => CerialAPI.GetAllCerial(context));
            });
            endpoints.MapGet("/getCerial/{cerialname}", async (context) =>
            {
                await Task.Run(() => CerialAPI.GetCerial(context));
            });
            endpoints.MapPut("/updateCerial/{id}", async (context) =>
            {
                await Task.Run(() => CerialAPI.UpdateCerial(context));
            });
            // endpoints.MapPost("", async (context) =>)
        });
    }
}
