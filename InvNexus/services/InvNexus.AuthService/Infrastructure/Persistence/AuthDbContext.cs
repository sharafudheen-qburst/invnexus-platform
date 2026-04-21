using InvNexus.AuthService.Domain.Entities;
using InvNexus.AuthService.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace InvNexus.AuthService.Infrastructure.Persistence;

public class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(256);

            entity.HasIndex(x => x.Email)
                .IsUnique();

            entity.Property(x => x.PasswordHash)
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(x => x.Role)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(x => x.IsActive)
                .IsRequired();

            var adminUserId = new Guid("9A769771-4F10-4528-AE6B-978B0A8BCBB8");

            entity.HasData(new User
            {
                Id = adminUserId,
                Email = "admin@invnexus.com",
                PasswordHash = "e86f78a8a3caf0b60d8e74e5942aa6d86dc150cd3c03338aef25b7d2d7e3acc7",
                Role = UserRole.Admin,
                IsActive = true
            });
        });
    }
}
