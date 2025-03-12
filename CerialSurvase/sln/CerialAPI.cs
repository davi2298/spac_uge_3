
using System.Net;
using Microsoft.AspNetCore.Mvc;

class CerialAPI
{
    public static CerialContext cerialContext = CerialContext.GetInstanse;

    /// <summary>
    /// Gets the first cerial with a given name
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static async void GetCerial(HttpContext context)
    {
        var cerialname = Convert.ToString(context.Request.RouteValues["cerialname"]);
        if (cerialname != null)
        {
            var cerial = cerialContext.Cerial
                .Where(c => c.Name == cerialname)
                .FirstOrDefault()
                ?? throw new ArgumentException("Cerial name not found");
            await context.Response.WriteAsJsonAsync(cerial);

        }
        return;
    }

    internal static async void GetAllCerial(HttpContext context)
    {
        var cerials = cerialContext.Cerial.ToArray();
        if (cerials.Length > 0)
        {
            await context.Response.WriteAsJsonAsync(cerials);
        }
    }

    internal static async Task UpdateCerial(HttpContext context)
    {
        var routeValue = context.Request.RouteValues["id"] ?? throw new ArgumentException("no given id");
        int cerialID = int.Parse(routeValue.ToString());

        Cerial toUpdateCerial = cerialContext.Cerial.Find(cerialID) ?? throw new ArgumentException("Cerial does not exist");
        Cerial updateWith = await context.Request.ReadFromJsonAsync<Cerial>() ?? throw new ArgumentException("No Cerial to update with");

        toUpdateCerial.Update(updateWith);
        cerialContext.SaveChanges();

        context.Response.StatusCode = (int)HttpStatusCode.Accepted;

    }
    internal static async Task FilterdGet(HttpContext context)
    {
        var calories = context.Request.Query["calories"];
        
        
    }

}