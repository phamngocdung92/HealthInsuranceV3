using HealthInsuranceV3.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HealthInsuranceV3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InsurancePackageController : Controller
    {
        private readonly HealthInsuranceContext _context;

        public InsurancePackageController(HealthInsuranceContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var packages = _context.InsurancePackages
                .Where(p => p.Dbstatus == true)
                .ToList();

            return View(packages);
        }

        public IActionResult Edit(int id)
        {
            var package = _context.InsurancePackages.Find(id);
            if (package == null)
            {
                return NotFound();
            }

            return View(package);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(InsurancePackage updatedPackage)
        {
            if (ModelState.IsValid)
            {
                var existingPackage = _context.InsurancePackages.Find(updatedPackage.PackageId);
                if (existingPackage == null)
                {
                    return NotFound();
                }

                existingPackage.PackageName = updatedPackage.PackageName;
                existingPackage.CoverageDetails = updatedPackage.CoverageDetails;
                existingPackage.PolicyTermInDay = updatedPackage.PolicyTermInDay;
                existingPackage.Price = updatedPackage.Price;

                _context.InsurancePackages.Update(existingPackage);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(updatedPackage);
        }

        public IActionResult Delete(int id)
        {
            var package = _context.InsurancePackages.Find(id);
            if (package == null)
            {
                return NotFound();
            }

            return View(package);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var package = _context.InsurancePackages.Find(id);
            if (package == null)
            {
                return NotFound();
            }

            package.Dbstatus = false;
            _context.InsurancePackages.Update(package);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
