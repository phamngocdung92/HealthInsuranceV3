using HealthInsuranceV3.Areas.Identity.Data;
using HealthInsuranceV3.Entities;

namespace HealthInsuranceV3.Models
{
    public class AdminEmpModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string DepartmentName { get; set; } = null!;
        public string? Email { get; set; }
        public virtual Department Department { get; set; }
    }
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
