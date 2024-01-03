namespace HealthInsuranceV3.Models
{
    public class UserInsuranceManagerModel
    {
        public int RegistrationId { get; set; }
        public string? EmployeeId { get; set; }
        public int? InsuranceId { get; set; }
        public int PackageId { get; set; }
        public string PackageName { get; set; } = null!;
        public string? RegistrationStatus { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string RegistrationDateFormatted { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string ApprovalDateFormatted { get; set; }
        public int? RejectionReasonId { get; set; }
    }
}