using AppResources.Messages;
using BL.Dtos;
using BL.Interfaces;
using DAL.Exceptions;
using DAL.UserModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Security.Claims;

namespace Ui.Services
{
    public class UserServices : IUser
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        readonly ILogger<UserServices> _logger;

        public UserServices(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IHttpContextAccessor httpContextAccessor, ILogger<UserServices> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async Task<UserResultDto> RegisterAsync(UserDto registerDto)
        {
            try
            {
                if (registerDto.Password != registerDto.ConfirmPassword)
                {
                    return new UserResultDto { Success = false, Errors = new[] {Messages.ConfirmPasswordMismatch } };
                }

                var user =  new ApplicationUser{Email = registerDto.Email, UserName = registerDto.Email };
                var result = await _userManager.CreateAsync(user, registerDto.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                }

                return new UserResultDto
                {
                    Success = result.Succeeded,
                    Errors = result.Errors?.Select(a => a.Description)
                };
            }
            catch (Exception ex)
            {
                var result = await _userManager.CreateAsync(new ApplicationUser { Email = registerDto.Email, UserName = registerDto.Email }, registerDto.Password);
                return new UserResultDto
                {
                    Success = false,
                    Errors = result.Errors.Select(a => a.Description)
                };
            }
            
        }

        public async Task<UserResultDto> LoginAsync(UserDto loginDto)
        {
            try
            {
                
                var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                    return new UserResultDto { Success = true, Token = "DummyToken",};

                return new UserResultDto
                {
                    Success = false,
                    Errors = new [] { Messages.LoginExceptionMessage }
                };
            }
            catch (Exception ex)
            {
                return new UserResultDto { Success = false, Errors = new[] { Messages.LoginExceptionMessage } };
            }
        }

        public async Task LogoutAsync()
        {
            try
            {
                await _signInManager.SignOutAsync();              
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, Messages.LogoutExceptionMessage, _logger);
            }
        }

        public async Task<UserDto> GetUserByIdAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userId);
                if (user == null) return null;

                return new UserDto
                {
                    Id = Guid.Parse(user.Id),
                    Email = user.Email,
                };
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, Messages.GetUserExceptionMessage, _logger);
            }
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync(UserDto loginDto)
        {
            try
            {
                var users = _userManager.Users;

                return users.Select(u => new UserDto
                { 
                    Id = Guid.Parse(u.Id),
                    Email = u.Email,
                });
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, Messages.AllUsersExceptionMessage, _logger);
            }
        }

        public Guid GetLoggedInUser()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.Parse(userId);
        }
    }
}
