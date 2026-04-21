using InvNexus.AuthService.Domain.Entities;

namespace InvNexus.AuthService.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}
