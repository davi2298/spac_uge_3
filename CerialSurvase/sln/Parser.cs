using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;

public class Parser
{
    private static Parser? instanse;
    private CerialContext dbContext;

    public static Parser GetInstanse => instanse ??= new();
    private Parser()
    {
        dbContext = CerialContext.GetInstanse;
    }

    public void AddCerial(Cerial cerial)
    {
        dbContext.Cerial.Add(cerial);
        dbContext.SaveChanges();
    }
    public void LoadFromCSV(string filePath)
    {
        if (!File.Exists(filePath))
            return;
        IEnumerable<string[]> lines = File.ReadAllLines(filePath)
            .Skip(2)
            .AsParallel()
            .Select(s => s.Split(';'))
            ;
        foreach (var line in lines)
        {
            Cerial newCerial = new(
                line[0],
                line[1].ToString(),
                line[2],
                int.Parse(line[3]),
                int.Parse(line[4]),
                int.Parse(line[5]),
                int.Parse(line[6]),
                float.Parse(line[7].Replace('.',',')),
                float.Parse(line[8].Replace('.',',')),
                int.Parse(line[9]),
                int.Parse(line[10]),
                int.Parse(line[11]),
                int.Parse(line[12]),
                float.Parse(line[13].Replace('.',',')),
                float.Parse(line[14].Replace('.',',')),
                float.Parse(line[15]) // TODO figure out
            );
            AddCerial(newCerial);
        }
    }
}

