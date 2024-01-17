using Microsoft.AspNetCore.Mvc;
using Waffles_Club.Service.Services.Interfaces;
using Waffles_Club.Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace Waffles_Club.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAll();
                return View("Users", users);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { RequestId = ex.Message });
            }
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Update(Guid userId)
        {
            try
            {
                var user = await _userService.GetById(userId.ToString());
                return View("Edit", user);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { RequestId = ex.Message });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Update(UpdateUserViewModel updateUserViewModel, Guid userId)
        {
            try
            {
                await _userService.UpdateAsync(userId.ToString(), updateUserViewModel);

                return Redirect("/Admin/User/GetAllUsers");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("UpdateError", ex.Message);

                var user = await _userService.GetById(userId.ToString());

                return View("Edit", user);
            }
        }
    }
}
