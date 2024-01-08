using HealthInsuranceV3.Entities;
using HealthInsuranceV3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HealthInsuranceV3.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
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
            var employees = _context.AspNetUsers
                .GroupJoin(_context.DepartmentManagers,
                    user => user.Id,
                    manager => manager.EmployeeId,
                    (user, managers) => new { user, managers })
                .SelectMany(j => j.managers.DefaultIfEmpty(), (j, manager) => new AdminEmpModel
                {
                    Id = j.user.Id,
                    FirstName = j.user.FirstName,
                    LastName = j.user.LastName,
                    PhoneNumber = j.user.PhoneNumber,
                    Email = j.user.Email,
                    DepartmentName = manager != null ? manager.Department.DepartmentName : ""
                })
                .ToList();

            return View(employees);
        }

        //Edit Emp
        public IActionResult Edit(string id)
        {
            var employee = _context.AspNetUsers
                .Where(u => u.Id == id)
                .Select(u => new EmployeeEditModel
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    DepartmentId = 0, 
                    Departments = new SelectList(_context.Departments, "DepartmentId", "DepartmentName")
                })
                .FirstOrDefault();

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Departments = new SelectList(_context.Departments, "DepartmentId", "DepartmentName");
                return View(model);
            }

            var employee = _context.AspNetUsers.Find(model.Id);

            if (employee == null)
            {
                return NotFound();
            }

            employee.FirstName = model.FirstName;
            employee.LastName = model.LastName;
            employee.Email = model.Email;
            employee.PhoneNumber = model.PhoneNumber;


            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        // Delete
        public IActionResult Delete(string id)
        {
            var employee = _context.AspNetUsers
                .Where(u => u.Id == id)
                .Select(u => new EmployeeEditModel
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber
                })
                .FirstOrDefault();

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var employee = _context.DepartmentManagers
                .Where(dm => dm.EmployeeId == id)
                .FirstOrDefault();

            if (employee != null)
            {
                // Cập nhật trạng thái DBStatus từ 1 thành 0
                employee.Dbstatus = false;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
