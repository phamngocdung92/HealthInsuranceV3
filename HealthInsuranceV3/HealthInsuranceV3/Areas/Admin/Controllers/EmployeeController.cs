using HealthInsuranceV3.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthInsuranceV3.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        private readonly HealthInsuranceContext _context;
        public EmployeeController(HealthInsuranceContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //var empData = _context.Admin
            return View();
        }
    }
}
