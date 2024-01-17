using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Waffles_Club.Data.Entity;
using Waffles_Club.Service.Services.Interfaces;
using Waffles_Club.Shared.ViewModels;

namespace Waffles_Club.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WaffleController : Controller
    {
        private readonly IWaffleService _waffleService;
        private readonly IFillingTypeService _fillingTypeService;
        private readonly IWaffleTypeService _waffleTypeService;

        public WaffleController(IWaffleService waffleService, IWaffleTypeService waffleTypeService, IFillingTypeService fillingTypeService)
        {
            _waffleService = waffleService;
            _waffleTypeService = waffleTypeService;
            _fillingTypeService = fillingTypeService;
        }
        private async Task UpdateViewBag()
        {
            var fillingTypes = await _fillingTypeService.GetAllAsync();
            var waffleTypes = await _waffleTypeService.GetAllAsync();
            ViewBag.FillingTypes = fillingTypes;
            ViewBag.WafflesTypes = waffleTypes;
        }
        [Authorize]
        public async Task<IActionResult> GetAllWaffles()
        {
            try
            {
                await UpdateViewBag();
                var waffles = await _waffleService.GetWaffleListAsync();
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
                await UpdateViewBag();
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
        public async Task<IActionResult> Update(WaffleViewModel viewModel,Guid waffleId)
        {
            await _waffleService.UpdateWaffleAsync(waffleId,viewModel);
            var waffles = await _waffleService.GetWaffleListAsync();
            return View("Waffles", waffles);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
    }
}
