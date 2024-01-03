using HealthInsuranceV3.Areas.Identity.Data;
using HealthInsuranceV3.Areas.User.Services.ListInsuranceService;
using HealthInsuranceV3.Areas.User.Services.UserInsuranceManagerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HealthInsuranceV3.Areas.User.Controllers
{
    [Authorize]
    [Area("User")]
    public class UserInsuranceManagerController : Controller
    {
        private readonly IUserInsuranceManagerService _UserInsuranceManagerService;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserInsuranceManagerController(IUserInsuranceManagerService UserInsuranceManagerService, UserManager<ApplicationUser> userManager)
        {
            _UserInsuranceManagerService = UserInsuranceManagerService;
            this._userManager = userManager;
        }
        public IActionResult Index()
        {
            var Id = _userManager.GetUserId(User);
            var data = _UserInsuranceManagerService.GetUserInsuranceRegistrations(Id);
            return View(data);
        }
        
        public IActionResult DeleteUserRegistration(int RegistrationId)
        {
            try
            {
                var Id = _userManager.GetUserId(User);
                ViewBag.CurrentUserID = Id;
                _UserInsuranceManagerService.DeleteUserInsuranceRegistration(Id, RegistrationId);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., display an error message)
                return RedirectToAction("Index"); // Redirect back to the list
            }
        }
    }
}
