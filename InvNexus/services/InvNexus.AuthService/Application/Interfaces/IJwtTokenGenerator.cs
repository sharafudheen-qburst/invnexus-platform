using InvNexus.AuthService.Domain.Entities;

namespace InvNexus.AuthService.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
