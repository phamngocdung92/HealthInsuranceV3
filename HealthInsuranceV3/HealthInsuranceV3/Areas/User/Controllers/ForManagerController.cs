using HealthInsuranceV3.Areas.Identity.Data;
using HealthInsuranceV3.Areas.User.Services.ForManagerService;
using HealthInsuranceV3.Areas.User.Services.ListInsuranceService;
using HealthInsuranceV3.Entities;
using HealthInsuranceV3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthInsuranceV3.Areas.User.Controllers
{
    [Authorize]
    [Area("User")]
    public class ForManagerController : Controller
    {
        private readonly IForManagerService _ForManagerService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly HealthInsuranceContext _context;

        public ForManagerController(IForManagerService ForManagerService, UserManager<ApplicationUser> userManager, HealthInsuranceContext dbContext)
        {
            _ForManagerService = ForManagerService;
            this._userManager = userManager;
            _context = dbContext;
        }
        public IActionResult Index()
        {
            var Id = _userManager.GetUserId(User);
            bool IsManager = _ForManagerService.IsUserManager(Id);
            var data = _ForManagerService.GetEmployeeList(Id, IsManager);
            return View(data);
        }
        public IActionResult CheckEmpInsurance(string Id)
        {
            var data = _ForManagerService.CheckEmpInsurance(Id);
            return View(data);
        }
        public IActionResult ApproveInsurance(int RegistrationId, string EmployeeId)
        {
            _ForManagerService.ApproveInsurance(RegistrationId, EmployeeId);
            return RedirectToAction("CheckEmpInsurance", new { Id = EmployeeId });
        }
        public IActionResult SelectReason(string EmployeeId, int RegistrationId)
        {
            var empData = _ForManagerService.CheckEmpInsurance(EmployeeId);
            var data = _ForManagerService.GetRejectionReasons();

            var viewModel = new SelectReasonModel
            {
                UserId = EmployeeId,
                RegistrationId = RegistrationId,
                Data = data
            };

            return View(viewModel);
        }
        public IActionResult RejectInsuranceRegistration(int RegistrationId, string EmployeeId, int RejectionReasonId)
        {
            _ForManagerService.RejectInsuranceRegistration(RegistrationId, EmployeeId, RejectionReasonId);
            return RedirectToAction("RejectInsuranceRegistration", new { Id = EmployeeId });
        }
        
    }
}
