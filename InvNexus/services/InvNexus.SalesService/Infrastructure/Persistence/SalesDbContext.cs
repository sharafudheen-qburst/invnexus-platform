using InvNexus.SalesService.Application.Interfaces;
using InvNexus.SalesService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvNexus.SalesService.Infrastructure.Persistence;

public class SalesDbContext(DbContextOptions<SalesDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<SalesOrder> SalesOrders => Set<SalesOrder>();
    public DbSet<SalesOrderItem> SalesOrderItems => Set<SalesOrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SalesOrder>(entity =>
        {
            entity.HasKey(salesOrder => salesOrder.Id);
            entity.Property(salesOrder => salesOrder.SalesNumber).IsRequired().HasMaxLength(50);
            entity.Property(salesOrder => salesOrder.Status).IsRequired().HasMaxLength(50);
            entity.HasIndex(salesOrder => salesOrder.SalesNumber).IsUnique();

            entity.HasMany(salesOrder => salesOrder.Items)
                .WithOne(item => item.SalesOrder)
                .HasForeignKey(item => item.SalesOrderId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<SalesOrderItem>(entity =>
        {
            entity.HasKey(item => item.Id);
            entity.Property(item => item.UnitPrice).HasColumnType("decimal(18,2)");
        });
    }
}
