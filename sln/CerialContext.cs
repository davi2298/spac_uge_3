using Microsoft.EntityFrameworkCore;
class CerialContext : DbContext
{
    private static CerialContext? instanse;
    public static CerialContext GetInstanse => instanse ??= new();
    
    private CerialContext(){}
    public DbSet<Cerial> Cerial { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL($"server=localhost;database={Environment.GetEnvironmentVariable("DBNAME")};user={Environment.GetEnvironmentVariable("DBUSER")};password={Environment.GetEnvironmentVariable("DBPASSWORD")}");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // modelBuilder.Entity<Cerial>(entity =>
        // {
        //     entity.Property(e => e.Name).IsRequired();
        //     entity.Property(e => e.mfr).IsRequired();
        //     entity.Property(e => e.Type).IsRequired();
        // });
;
    }
}