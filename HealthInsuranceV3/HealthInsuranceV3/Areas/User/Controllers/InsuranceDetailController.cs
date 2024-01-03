using HealthInsuranceV3.Areas.Identity.Data;
using HealthInsuranceV3.Areas.User.Services.InsuranceDetailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HealthInsuranceV3.Areas.User.Controllers
{
	[Authorize]
    [Area("User")]
    public class InsuranceDetailController : Controller
	{
		private readonly IInsuranceDetailService _InsuranceDetailService;
		private readonly UserManager<ApplicationUser> _userManager;

		public InsuranceDetailController(IInsuranceDetailService InsuranceDetailService, UserManager<ApplicationUser> userManager)
		{
			_InsuranceDetailService = InsuranceDetailService;
			this._userManager = userManager;
		}

		public IActionResult Index(int InsuranceId)
		{
			var UserId = _userManager.GetUserId(this.User);
			ViewBag.CurrentUserID = UserId;
			var data = _InsuranceDetailService.GetInsuranceDetail(InsuranceId);
			return View(data);
		}
		//[HttpPost]
		public IActionResult RegisterInsurance(int PackageID)
		{
			var UserId = _userManager.GetUserId(User);
			_InsuranceDetailService.RegisterInsuranceForEmployee(UserId, PackageID);
			return RedirectToAction("RegistrationSuccess");
		}
        public IActionResult RegistrationSuccess()
        {
            return View();
        }
    }
}
