using HealthInsuranceV3.Areas.User.Services.ListInsuranceService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthInsuranceV3.Areas.User.Controllers
{
    [Authorize]
    [Area("User")]
    public class ListInsuranceController : Controller
    {
        private readonly IListInsuranceService _ListInsuranceService;

        public ListInsuranceController(IListInsuranceService ListInsuranceService)
        {
            _ListInsuranceService = ListInsuranceService;
        }
        public IActionResult Index()
        {
            var data = _ListInsuranceService.GetInsuranceList();
            return View(data);
        }
    }
}
