using Microsoft.AspNetCore.Mvc;
using WebApiMovies.DTOs.AuthDTOs;
using WebApiMovies.Services;

namespace WebApiMovies.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }

        [HttpPost("register", Name = "RegisterUser")]
        public async Task<ActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            await _authService.Register(userRegisterDto);
            return StatusCode(201);
        }

        [HttpPost("login", Name = "LoginUser")]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] UserLoginDto userLoginDto)
        {
            var token = await _authService.Login(userLoginDto);
            return token;
        }

        [HttpPost("confirmEmail", Name = "ConfirmEmail")]
        public async Task<ActionResult> ConfirmEmail([FromBody] ConfirmEmailDto confirmEmailDto)
        {
            await _authService.ConfirmEmail(confirmEmailDto);
            return Ok();
        }

        [HttpPost("forgotPassword", Name ="ForgotPassword")]
        public async Task<ActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            await _authService.ForgotPassword(forgotPasswordDto);
            return Ok();
        }

        [HttpPost("resetPassword", Name ="ResetPassword")]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            await _authService.ResetPassword(resetPasswordDto);
            return Ok();
        }

        [HttpPost("twoStepVerification", Name = "TwoStepVerification")]
        public async Task<ActionResult<AuthResponseDto>> TwoStepVerification([FromBody] TwoFactorDto twoFactorDto)
        {
            var token = await _authService.TwoStepVerification(twoFactorDto);
            return token;
        }
    }
}
