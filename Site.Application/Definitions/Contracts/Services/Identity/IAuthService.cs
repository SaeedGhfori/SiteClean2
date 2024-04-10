using Site.Application.Definitions.Dtos.Identity;

namespace Site.Application.Definitions.Contracts.Services.Identity
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request);
        Task<RegistrationResponse> Register(RegisterationRequest request);
        Task Logout();
        Task<PasswordResponse> ChangePassword(ChangePasswordRequest request);
        Task<PasswordResponse> ForgotPassword(ForgotsPasswordRequest request);
    }
}
