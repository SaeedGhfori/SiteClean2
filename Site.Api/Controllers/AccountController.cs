using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Site.Application.Definitions.Contracts.Services.Identity;
using Site.Application.Definitions.Dtos.Identity;

namespace Site.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService authService;

        public AccountController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
        {
            return Ok(await authService.Login(request));
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> Register(RegisterationRequest request)
        {
            return Ok(await authService.Register(request));
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await authService.Logout();
            return Ok();
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            var result = await authService.ChangePassword(request);
            if (result.Success)
            {
                return Ok();
            }
            return BadRequest(result.Message);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotsPasswordRequest request)
        {
            var result = await authService.ForgotPassword(request);
            if (result.Success)
            {
                return Ok();
            }
            return BadRequest(result.Message);
        }

    }
}
