using System.Net;
using Google.Protobuf.WellKnownTypes;

public class CerialDriver
{
    private static void Main(string[] args)
    {
        var tmp = DotEnv.TryGetSolutionDirectoryInfo();
        DotEnv.Load(Path.Combine(DotEnv.TryGetSolutionDirectoryInfo().FullName, ".env"));

        if (CerialContext.GetInstanse.Cerial.Count() == 0)
        {
            Parser.GetInstanse.LoadFromCSV(Path.Combine(DotEnv.TryGetSolutionDirectoryInfo().FullName, "Data\\Cereal.csv"));
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
            endpoints.MapPut("", async (context) =>
            {
                await Task.Run(() => CerialAPI.UpdateCerial(context));
            });
            // endpoints.MapPost("", async (context) =>)
        });
    }
}
