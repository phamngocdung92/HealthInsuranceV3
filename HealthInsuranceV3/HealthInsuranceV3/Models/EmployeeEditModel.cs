using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HealthInsuranceV3.Models
{
    public class EmployeeEditModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public int DepartmentId { get; set; }
        public SelectList Departments { get; set; }
        public string DepartmentName { get; set; } 
    }
}
