using Microsoft.EntityFrameworkCore;
public class CerialContext : DbContext
{
    private static CerialContext? instanse;
    public static CerialContext GetInstanse => instanse ??= new();
    
    private CerialContext(){}
    public DbSet<Cerial> Cerial { get; set; }
    public static bool IsTesting { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!IsTesting)
            optionsBuilder.UseMySQL($"server=localhost;database={Environment.GetEnvironmentVariable("DBNAME")};user={Environment.GetEnvironmentVariable("DBUSER")};password={Environment.GetEnvironmentVariable("DBPASSWORD")}");
        else
            optionsBuilder.UseInMemoryDatabase("Testing");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public static void Reset(){
        if (IsTesting) instanse = null;
    }

}