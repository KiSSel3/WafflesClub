using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Waffles_Club.Service.Services.Interfaces;

namespace Waffles_Club.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users=await _userService.GetAll();
            return View(users);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}