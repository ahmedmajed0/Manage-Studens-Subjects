using AppEnums;
using AppResources.Messages;
using BL.Dtos;
using BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Ui.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly IUser _userService;
        public AccountController(IUser userService)
        {
            _userService = userService;
        }
        public IActionResult Register()
        {
            return View(new UserDto());
        }

        public IActionResult Login(string? returnUrl)
        {
            return View(new UserDto() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(UserDto user)
        {
            //if true thats mean login process
            if (user.ConfirmPassword.IsNullOrEmpty())
            {
                var result = await _userService.LoginAsync(user);
                if (result.Success)
                {
                    TempData["MeesageType"] = MeesagesType.LoginSuccess;

                    var returnUrl =  user.ReturnUrl.IsNullOrEmpty() ? "~/Admin" : user.ReturnUrl; // if(true) = returnUrl = "~/Admin" else  = user.ReturnUrl
                    return Redirect(returnUrl);
                }

                TempData["MeesageType"] = MeesagesType.LoginFailed;
                return View(nameof(Login), user);
            }
            else //register process
            {
                if (!ModelState.IsValid)
                {
                    TempData["MeesageType"] = MeesagesType.RegisterFailed;
                    return RedirectToAction(nameof(Register), user);
                }
                var reault = await _userService.RegisterAsync(user);
                if (reault.Success)
                {
                    TempData["MeesageType"] = MeesagesType.RegisterSuccess;
                    return Redirect("~/Admin");
                }
                TempData["MeesageType"] = MeesagesType.RegisterFailed;
                return RedirectToAction(nameof(Register), user);
            }

        }

        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutAsync();
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
