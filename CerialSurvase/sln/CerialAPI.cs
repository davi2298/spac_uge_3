
using System.Net;
using System.IO;
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
    public static void GetCerial(HttpContext context)
    {
        var cerialname = Convert.ToString(context.Request.RouteValues["cerialname"]);
        if (cerialname != null)
        {
            var cerial = cerialContext.Cerial.ToList()
                .Where(c => c.Name.Equals(cerialname, StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefault()
                ?? throw new ArgumentException("Cerial name not found");
            context.Response.WriteAsJsonAsync(cerial);

        }
        return;
    }

    internal static async Task GetAllCerial(HttpContext context)
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
        Cerial updateWith = await context.Request.ReadFromJsonAsync<Cerial>() ?? throw new ArgumentException("No Cerial to update with");
        var updatedCerial = await updateCerialFromId(updateWith, cerialID);

        context.Response.StatusCode = (int)HttpStatusCode.Accepted;
        await context.Response.WriteAsJsonAsync(updatedCerial);

    }

    internal static async Task FilterdGet(HttpContext context)
    {
        List<Func<Cerial, bool>> filters = [];
        String? caloriesStirng = context.Request.Query["calories"];
        if (caloriesStirng != null)
        {
            filters.Add(c => c.Calories == Convert.ToInt32(caloriesStirng));
        }

        var tmp = cerialContext.Cerial.First();
        Func<Cerial, bool> finalFilter = c => filters.Aggregate(true, (acc, a) => acc && a.Invoke(c));
        var b = filters.Aggregate(true, (acc, a) => acc && a.Invoke(tmp));
        var found = cerialContext.Cerial.ToList().Where(c => finalFilter.Invoke(c)).ToList();

        await context.Response.WriteAsJsonAsync(found);
    }

    internal static void Delete(HttpContext context)
    {
        var cerialId = Convert.ToInt32(context.Request.RouteValues["id"]);
        var cerial = cerialContext.Cerial.Where(c => c.ID == cerialId).First();
        cerialContext.Cerial.Remove(cerial);
        cerialContext.SaveChanges();
    }

    internal static async Task NewCerialAsync(HttpContext context)
    {
        var cerial = await context.Request.ReadFromJsonAsync<Cerial>() ?? throw new ArgumentException("No body given");

        if (cerial.ID != null)
        {
            var updatedCerial = await updateCerialFromId(cerial, (int)cerial.ID);
            context.Response.StatusCode = (int)HttpStatusCode.Accepted;
            await context.Response.WriteAsJsonAsync(updatedCerial);

            return;
        }
        var newCerial = cerialContext.Cerial.Add(cerial).Entity;
        cerialContext.SaveChanges();
        context.Response.StatusCode = (int)HttpStatusCode.Created;
        await context.Response.WriteAsJsonAsync(newCerial);
    }


    internal static async Task GetImageForCerial(HttpContext context, string pathToDataFolder)
    {
        var cerialname = Convert.ToString(context.Request.RouteValues["cerialname"]) ?? throw new ArgumentException("No cerial name given");
        var pathToImg = Path.Combine(pathToDataFolder, $"Cerial pictures\\{cerialname}.jpg");
        var dir = Directory.GetFiles(pathToDataFolder, "*.jpg", SearchOption.AllDirectories);
        var img = dir.AsParallel().Where(e => e.Contains(cerialname, StringComparison.CurrentCultureIgnoreCase)).First() ?? throw new FileNotFoundException();
        await context.Response.SendFileAsync(img);
    }
    private static async Task<Cerial> updateCerialFromId(Cerial updateWith, int cerialID)
    {
        Cerial toUpdateCerial = cerialContext.Cerial.Find(cerialID) ?? throw new ArgumentException("Cerial does not exist");

        toUpdateCerial.Update(updateWith);
        await cerialContext.SaveChangesAsync();
        return toUpdateCerial;
    }


}