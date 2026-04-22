using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace InvNexus.PurchaseService.Infrastructure.Persistence;

public class PurchaseDbContextFactory : IDesignTimeDbContextFactory<PurchaseDbContext>
{
    public PurchaseDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PurchaseDbContext>();
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=InvNexusPurchaseDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");

        return new PurchaseDbContext(optionsBuilder.Options);
    }
}
