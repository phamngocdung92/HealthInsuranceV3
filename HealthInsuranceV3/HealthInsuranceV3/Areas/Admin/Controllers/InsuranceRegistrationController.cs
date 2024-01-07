using Microsoft.AspNetCore.Mvc;

namespace HealthInsuranceV3.Areas.Admin.Controllers
{
    public class InsuranceRegistrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
