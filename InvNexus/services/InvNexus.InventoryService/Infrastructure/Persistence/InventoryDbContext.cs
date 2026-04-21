using InvNexus.InventoryService.Application.Interfaces;
using InvNexus.InventoryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvNexus.InventoryService.Infrastructure.Persistence;

public class InventoryDbContext(DbContextOptions<InventoryDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Stock> Stocks => Set<Stock>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(product => product.Id);
            entity.Property(product => product.Name).IsRequired().HasMaxLength(200);
            entity.Property(product => product.Price).HasColumnType("decimal(18,2)");

            entity.HasOne(product => product.Stock)
                .WithOne(stock => stock.Product)
                .HasForeignKey<Stock>(stock => stock.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(stock => stock.Id);
            entity.HasIndex(stock => stock.ProductId).IsUnique();
        });
    }
}
