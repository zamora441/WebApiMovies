using Microsoft.AspNetCore.Mvc;
using WebApiMovies.DTOs.AuthDTOs;

namespace WebApiMovies.Services
{
    public interface IAuthService
    {
        public Task Register(UserRegisterDto userRegisterDto);
        public Task<AuthResponseDto> Login(UserLoginDto userLoginDto);
        public Task ConfirmEmail(ConfirmEmailDto confirmEmailDto);
        public Task ForgotPassword(ForgotPasswordDto forgotPasswordDto);
        public Task ResetPassword (ResetPasswordDto resetPasswordDto);
        public Task<AuthResponseDto> TwoStepVerification(TwoFactorDto twoFactorDto);
    }
}
