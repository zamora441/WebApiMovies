using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiMovies.Data.Entities;
using WebApiMovies.DTOs;
using WebApiMovies.DTOs.AuthDTOs;
using WebApiMovies.Exceptions;

namespace WebApiMovies.Services.Implements
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public AuthService(UserManager<User> userManager, IMapper mapper, IConfiguration configuration, IEmailService emailService)
        {
            this._userManager = userManager;
            this._mapper = mapper;
            this._configuration = configuration;
            this._emailService = emailService;
        }

        public async Task Register(UserRegisterDto userRegisterDto)
        {
            var user = _mapper.Map<User>(userRegisterDto);

            var result = await _userManager.CreateAsync(user, userRegisterDto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new BadRequestException(errors);
            }


            if (userRegisterDto.Is2StepVerificacionEnabled)
            {
                await _userManager.SetTwoFactorEnabledAsync(user, true);
            }

            var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var param = new Dictionary<string, string?>
            {
                { "token", confirmEmailToken},
                { "email", user.Email }
            };
            var confirmEmailUrl = QueryHelpers.AddQueryString(userRegisterDto.ClientUri, param);
            var emailMessage = new EmailMessageDto(new List<string> { user.Email }, "Email confirmation.", confirmEmailUrl);
            await _emailService.SendEmailAsync(emailMessage);

            await _userManager.AddToRoleAsync(user, "Administrator");
        }

        public async Task<AuthResponseDto> Login(UserLoginDto userLoginDto)
        {
            var user = await AuthenticateUser(userLoginDto);

            if(await _userManager.GetTwoFactorEnabledAsync(user))
            {
                return await GenerateOTPFor2StepVerification(user);
            }

            var token = await GenerateSecurityToken(userLoginDto.UserName);

            await _userManager.ResetAccessFailedCountAsync(user);

            return new AuthResponseDto
            {
                Token = token,
                Is2StepVerificationRequired = false
            };

        }

        public async Task ConfirmEmail(ConfirmEmailDto confirmEmailDto)
        {
            var user = await _userManager.FindByEmailAsync(confirmEmailDto.Email);
            if (user is null)
            {
                throw new BadRequestException("Invalid Email Confirmation Request");
            }

            var confirmEmailResult = await _userManager.ConfirmEmailAsync(user, confirmEmailDto.Token);
            if (!confirmEmailResult.Succeeded)
            {
                Console.WriteLine(string.Join(",", confirmEmailResult.Errors.Select(e => e.Description)));
                throw new BadRequestException("Confirm token is invalid or expired");
            }
        }

        public async Task ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
            if(user is null){
                throw new BadHttpRequestException("Invalid Request");
            }

            var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var param = new Dictionary<string, string?>
            {
                {"token", resetPasswordToken },
                {"email", forgotPasswordDto.Email}
            };

            var resetPasswordUrl = QueryHelpers.AddQueryString(forgotPasswordDto.ClientUri, param);

            var emailMessage = new EmailMessageDto(new List<string>{ user.Email }, "Reset password token.", resetPasswordUrl);

            await  _emailService.SendEmailAsync(emailMessage);
        }

        public async Task ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if(user is null)
            {
                throw new BadHttpRequestException("Invalid Request");
            }

            var passwordValdiator = _userManager.PasswordValidators.FirstOrDefault();
            var isPasswordValid = await passwordValdiator!.ValidateAsync(_userManager, user, resetPasswordDto.Password);
            if(!isPasswordValid.Succeeded)
            {
                var errors = string.Join(",", isPasswordValid.Errors.Select(e => e.Description));
                throw new BadRequestException(errors);
            }

            var resetPasswordResult = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
            if (!resetPasswordResult.Succeeded)
            {
                throw new BadRequestException("Reset token is invalid or expired.");
            }

            await _userManager.SetLockoutEndDateAsync(user, new DateTime(2000, 1, 1));

            var emailMessage = new EmailMessageDto(new List<string> { user.Email }, "Change of password.", "Your password has been changed.");
            await _emailService.SendEmailAsync(emailMessage);
            
        }

        private async Task<User> AuthenticateUser(UserLoginDto userLoginDto)
        {
            var user = await _userManager.FindByNameAsync(userLoginDto.UserName);

            if(user is null)
            {
                throw new BadRequestException("Invalid username or password.");
            }

            if(!await _userManager.IsEmailConfirmedAsync(user))
            {
                throw new UnauthorizedException("The email has not been confirmed yet.");
            }

            if(! await _userManager.CheckPasswordAsync(user, userLoginDto.Password))
            {
                await HandleFailedLoginAttempt(user, userLoginDto.ClientUri);
                throw new BadRequestException("Invalid username or password.");
            }

            return user;
        }

        private async Task<string> GenerateSecurityToken(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim("userId", user.Id),
            };
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["KeyJWT"]!));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var securityToken = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        private async Task HandleFailedLoginAttempt(User user, string clientUri)
        {
            await _userManager.AccessFailedAsync(user);
            if(await _userManager.IsLockedOutAsync(user))
            {
                var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var param = new Dictionary<string, string?>
                {
                    {"token", resetPasswordToken },
                    {"email", user.Email}
                };

                var resetPasswordUrl = QueryHelpers.AddQueryString(clientUri, param);
                var contentMessage = $"Your account is locked out. To reset the password click this link: {resetPasswordUrl}";
                var emailMessgae = new EmailMessageDto(new List<string> { user.Email }, "Locked out account information", contentMessage);
                
                await _emailService.SendEmailAsync(emailMessgae);

                throw new UnauthorizedException("The account is locked out.");
            }
        }

        private async Task<AuthResponseDto> GenerateOTPFor2StepVerification(User user)
        {
            var providers = await _userManager.GetValidTwoFactorProvidersAsync(user);
            if (!providers.Contains("Email"))
            {
                throw new UnauthorizedException("Invalid 2-Step Verification Provider.");
            }

            var authentificationToken = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
            var emailMessage = new EmailMessageDto(new List<string> { user.Email }, "Authentification token", authentificationToken);
            await _emailService.SendEmailAsync(emailMessage);

            return new AuthResponseDto {Is2StepVerificationRequired = true, Provider = "Email" };
        }

        public async Task<AuthResponseDto> TwoStepVerification(TwoFactorDto twoFactorDto)
        {
            var user = await _userManager.FindByNameAsync(twoFactorDto.UserName);
            if(user is null)
            {
                throw new BadRequestException("Invalid Request.");
            }
            Console.WriteLine(twoFactorDto.Provider);
            Console.WriteLine(twoFactorDto.Token);
            var tokenVerification = await _userManager.VerifyTwoFactorTokenAsync(user, twoFactorDto.Provider, twoFactorDto.Token);
            if (!tokenVerification)
            {
                throw new BadRequestException("Invalid Token Verification.");
            }

            var token = await GenerateSecurityToken(user.UserName);

            return new AuthResponseDto { Is2StepVerificationRequired = false, Token = token };
        }
    }
}
