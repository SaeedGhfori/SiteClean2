using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Site.Application.Definitions.Constants;
using Site.Application.Definitions.Contracts.Services.Identity;
using Site.Application.Definitions.Dtos.Identity;
using Site.Domain.Entities.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Site.Application.Implementations.Services.Identity
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthService(UserManager<ApplicationUser> userManager,
            IOptions<JwtSettings> jwtSettings,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _signInManager = signInManager;
        }


        #region Register
        public async Task<RegistrationResponse> Register(RegisterationRequest request)
        {
            var existingUser = await _userManager.FindByNameAsync(request.UserName);
            if (existingUser != null)
            {
                throw new Exception($"user name '{request.UserName}' already exists.");
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                NationalCode = request.NationalCode,
                EmailConfirmed = true
            };


            var existingEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existingEmail == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Employee");
                    return new RegistrationResponse() { UserId = user.Id };
                }
                else
                {
                    throw new Exception($"{result.Errors}");
                }
            }
            else
            {
                throw new Exception($"Email '{request.Email}' already exists.");
            }
        }
        #endregion

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new Exception($"user with {request.Email} not fount.");
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                throw new Exception($"credentials for {request.Email} arent valid.");
            }

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            AuthResponse response = new AuthResponse()
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                UserName = user.UserName,
            };

            return response;

        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(CustomClaimTypes.Uid,user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<PasswordResponse> ChangePassword(ChangePasswordRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return new PasswordResponse { Success = false, Message = "User not found." };
            }

            var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                return new PasswordResponse { Success = false, Message = "Error occurred while changing password." };
            }

            return new PasswordResponse { Success = true, Message = "Password changed successfully." };
        }

        public async Task<PasswordResponse> ForgotPassword(ForgotsPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new PasswordResponse { Success = false, Message = "User not found." };
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            // ارسال ایمیل با توکن بازیابی 

            return new PasswordResponse { Success = true, Message = "Reset token sent to email." };
        }
    }
}
