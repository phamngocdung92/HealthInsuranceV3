using HealthInsuranceV3.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HealthInsuranceV3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InsuranceCompanyController : Controller
    {
        private readonly HealthInsuranceContext _context;

        public InsuranceCompanyController(HealthInsuranceContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var companies = _context.InsuranceCompanies
                .Where(c => c.Dbstatus == true)
                .ToList();

            return View(companies);
        }
        //Edit

        public IActionResult Edit(int id)
        {
            var company = _context.InsuranceCompanies
                .Where(c => c.CompanyId == id)
                .FirstOrDefault();

            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(InsuranceCompany updatedCompany)
        {
            if (ModelState.IsValid)
            {
                
                var existingCompany = _context.InsuranceCompanies
                    .Where(c => c.CompanyId == updatedCompany.CompanyId)
                    .FirstOrDefault();

                if (existingCompany == null)
                {
                    return NotFound();
                }
                existingCompany.CompanyName = updatedCompany.CompanyName;
                existingCompany.EstablishedYear = updatedCompany.EstablishedYear;
                existingCompany.ContactInfo = updatedCompany.ContactInfo;
                existingCompany.Address = updatedCompany.Address;

                _context.InsuranceCompanies.Update(existingCompany);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(updatedCompany);
        }

        //Delete
        public IActionResult Delete(int id)
        {
            var company = _context.InsuranceCompanies
                .Where(c => c.CompanyId == id)
                .FirstOrDefault();

            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var company = _context.InsuranceCompanies
                .Where(c => c.CompanyId == id)
                .FirstOrDefault();

            if (company != null)
            {
               
                company.Dbstatus = false;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
