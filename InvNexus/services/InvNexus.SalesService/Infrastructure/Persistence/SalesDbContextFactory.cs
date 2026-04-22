using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace InvNexus.SalesService.Infrastructure.Persistence;

public class SalesDbContextFactory : IDesignTimeDbContextFactory<SalesDbContext>
{
    public SalesDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SalesDbContext>();
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=InvNexusSalesDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");

        return new SalesDbContext(optionsBuilder.Options);
    }
}
