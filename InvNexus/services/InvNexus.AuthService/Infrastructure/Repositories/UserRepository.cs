using InvNexus.AuthService.Application.Interfaces;
using InvNexus.AuthService.Domain.Entities;
using InvNexus.AuthService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InvNexus.AuthService.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AuthDbContext _dbContext;

    public UserRepository(AuthDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var normalizedEmail = email.Trim().ToLowerInvariant();

        return _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email.ToLower() == normalizedEmail, cancellationToken);
    }
}
