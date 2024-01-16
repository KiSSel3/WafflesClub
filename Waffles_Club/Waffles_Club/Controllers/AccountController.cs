using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Waffles_Club.Service.Services.Interfaces;
using Waffles_Club.Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Waffles_Club.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService) =>
            (_userService) = (userService);

        [HttpGet]
        public IActionResult Authorization()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                var response = await _userService.LoginAsync(model);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(response));

                return Redirect("/");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("LoginError", ex.Message);

                return View("Authorization");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                var response = await _userService.RegisterAsync(model);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(response));

                return Redirect("/");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("LoginError", ex.Message);

                return View("Authorization");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAccountPage()
        {
            try
            {
                string userId = User.FindFirst("UserId")?.Value;
                var user = await _userService.GetById(userId);
                return View(new UserViewModel { Name = user.Name, Email = user.Email, });
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { RequestId = ex.Message });
            }
            

        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}
