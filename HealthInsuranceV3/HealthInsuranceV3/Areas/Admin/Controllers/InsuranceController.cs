using HealthInsuranceV3.Entities;
using HealthInsuranceV3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthInsuranceV3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InsuranceController : Controller
    {
        private readonly HealthInsuranceContext _context;

        public InsuranceController(HealthInsuranceContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var insurances = _context.Insurances.ToList();
            return View(insurances);
        }

        public IActionResult Edit(int id)
        {
            var insurance = _context.Insurances.Find(id);
            if (insurance == null)
            {
                return NotFound();
            }
            return View(insurance);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Insurance updatedInsurance)
        {
            if (ModelState.IsValid)
            {
                var existingInsurance = _context.Insurances.Find(updatedInsurance.InsuranceId);
                if (existingInsurance == null)
                {
                    return NotFound();
                }

                
                existingInsurance.InsuranceName = updatedInsurance.InsuranceName;
                existingInsurance.IconText = updatedInsurance.IconText;
                existingInsurance.CompanyId = updatedInsurance.CompanyId;
                existingInsurance.Description = updatedInsurance.Description;

                _context.Insurances.Update(existingInsurance);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(updatedInsurance);
        }

        public IActionResult Delete(int id)
        {
            var insurance = _context.Insurances.Find(id);
            if (insurance == null)
            {
                return NotFound();
            }

            insurance.Dbstatus = false;
            _context.Insurances.Update(insurance);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
