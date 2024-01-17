using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Waffles_Club.Service.Services.Interfaces;
using Waffles_Club.Shared.ViewModels;

namespace Waffles_Club.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WaffleController : Controller
    {
        private readonly IWaffleService _waffleService;

        public WaffleController(IWaffleService waffleService)
        {
            _waffleService = waffleService;
        }
        [Authorize]
        public async Task<IActionResult> GetAllWaffles()
        {
            try
            {
                var waffles= await _waffleService.GetWaffleListAsync();
                return View("Waffles", waffles);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { RequestId = ex.Message });
            }
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Update(Guid waffleId)
        {
            try
            {
                var waffle= await _waffleService.GetWaffleByIdAsync(waffleId);
                return View("Edit", waffle);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { RequestId = ex.Message });
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Update()
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
    }
}
