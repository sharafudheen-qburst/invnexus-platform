using InvNexus.AuthService.Application.DTOs;

namespace InvNexus.AuthService.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto?> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken = default);
}
