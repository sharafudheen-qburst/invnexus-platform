using InvNexus.PurchaseService.Application.Interfaces;
using InvNexus.PurchaseService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvNexus.PurchaseService.Infrastructure.Persistence;

public class PurchaseDbContext(DbContextOptions<PurchaseDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<PurchaseOrder> PurchaseOrders => Set<PurchaseOrder>();
    public DbSet<PurchaseOrderItem> PurchaseOrderItems => Set<PurchaseOrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PurchaseOrder>(entity =>
        {
            entity.HasKey(purchaseOrder => purchaseOrder.Id);
            entity.Property(purchaseOrder => purchaseOrder.PurchaseNumber).IsRequired().HasMaxLength(50);
            entity.Property(purchaseOrder => purchaseOrder.Status).IsRequired().HasMaxLength(50);
            entity.HasIndex(purchaseOrder => purchaseOrder.PurchaseNumber).IsUnique();

            entity.HasMany(purchaseOrder => purchaseOrder.Items)
                .WithOne(item => item.PurchaseOrder)
                .HasForeignKey(item => item.PurchaseOrderId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<PurchaseOrderItem>(entity =>
        {
            entity.HasKey(item => item.Id);
            entity.Property(item => item.UnitPrice).HasColumnType("decimal(18,2)");
        });
    }
}
