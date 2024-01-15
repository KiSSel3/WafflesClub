using Microsoft.AspNetCore.Mvc;
using Waffles_Club.Service.Services.Interfaces;
using Waffles_Club.Shared.ViewModels;

namespace Waffles_Club.Controllers
{
    public class WaffleTypeController : Controller
    {
        private readonly IWaffleTypeService _waffleTypeService;

        public WaffleTypeController(IWaffleTypeService waffleTypeService)
        {
            _waffleTypeService = waffleTypeService;
        }
        public async Task<ActionResult> GetAllTypes() 
        {
            try
            {
                var types=await _waffleTypeService.GetAllAsync();
                return View(types);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { RequestId = ex.Message });
            }
        }
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var type=await _waffleTypeService.GetByIdAsync(id);
                return View(type);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { RequestId = ex.Message });
            }
        }
    }
}
