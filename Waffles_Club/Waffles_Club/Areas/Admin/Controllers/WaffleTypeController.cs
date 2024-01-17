using Microsoft.AspNetCore.Mvc;
using Waffles_Club.Data.Entity;
using Waffles_Club.Service.Services.Implementations;
using Waffles_Club.Service.Services.Interfaces;
using Waffles_Club.Shared.ViewModels;

namespace Waffles_Club.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WaffleTypeController : Controller
    {
        private readonly IWaffleTypeService _waffleTypeService;

        public WaffleTypeController(IWaffleTypeService waffleTypeService)
        {
            _waffleTypeService = waffleTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var waffleTypes = await _waffleTypeService.GetAllAsync();

                return View("WaffleTypes", waffleTypes);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { RequestId = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid typeId)
        {
            try
            {
                await _waffleTypeService.DeleteAsync(typeId);

                return Redirect("/Admin/WaffleType/Index");
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { RequestId = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(TypeViewModel typeViewModel)
        {
            try
            {
                await _waffleTypeService.CreateAsync(typeViewModel);

                return Redirect("/Admin/WaffleType/Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("CreateError", ex.Message);  

                return View("Create");
            }
        }
    }
}
