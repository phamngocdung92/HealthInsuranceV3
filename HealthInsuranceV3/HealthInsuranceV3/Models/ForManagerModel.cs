namespace HealthInsuranceV3.Models
{
    public class ForManagerModel
    {
        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string DepartmentName { get; set; } = null!;
        public int RegistrationId { get; set; }
        public string? EmployeeId { get; set; }
        public int? InsuranceId { get; set; }
        public int PackageId { get; set; }
        public string PackageName { get; set; } = null!;
        public string? RegistrationStatus { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime? ApprovalDate { get; set; }

        public int? RejectionReasonId { get; set; }
        public string ReasonText { get; set; } = null!;
        public int ReasonId { get; set; }
        public int? DepartmentId { get; set; }
        public string? ManagerId { get; set; }
        public bool IsManager { get; set; }

    }
}