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
            TempData["SuccessMessage"] = "Approval Success!";
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
        public IActionResult ForHR()
        {
            var Id = _userManager.GetUserId(User);
            bool IsManager = _ForManagerService.IsUserManager(Id);

            // Fetch additional data needed for the DepartmentAndManager model
            var dataDepartment = _ForManagerService.GetDepartments();
            var dataManager = _ForManagerService.GetManagers();

            // Create an instance of DepartmentAndManager and set its properties
            var viewModel = new DepartmentAndManager
            {
                Data = _ForManagerService.GetNewEmp(Id, IsManager),
                dataDepartment = dataDepartment,
                dataManager = dataManager
            };

            return View(viewModel);
        }

        //public IActionResult SelectDepartAndManager()
        //{
        //    var Id = _userManager.GetUserId(User);
        //    bool IsManager = _ForManagerService.IsUserManager(Id);
        //    var dataDepartment = _ForManagerService.GetDepartments();
        //    var dataManager = _ForManagerService.GetManagers();
        //    var data = _ForManagerService.GetNewEmp(Id, IsManager);
        //    var viewModel = new DepartmentAndManager
        //    {
        //        Data = data,
        //        dataDepartment = dataDepartment,
        //        dataManager = dataManager
        //    };

        //    return View(viewModel);
        //}

        public IActionResult UpdateEmployeeDepartment(string Id, string EmployeeId, int ManagerId, int DepartmentId, bool IsManager)
        {
            
            //var data = _ForManagerService.GetNewEmp(Id, IsManager);
            _ForManagerService.UpdateEmployeeDepartment(Id, EmployeeId, ManagerId, DepartmentId);
            return RedirectToAction("ForHR");
        }
    }
}
