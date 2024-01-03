namespace HealthInsuranceV3.Models
{
    public class ListInsuranceModel
    {
        public int InsuranceId { get; set; }
        public string? IconText { get; set; }
        public string InsuranceName { get; set; } = null!;
        public string? Description { get; set; }
        public string CompanyName { get; set; } = null!;
        public string? ContactInfo { get; set; }
        public string? Address { get; set; }
        public DateTime? EstablishedYear { get; set; }
        public string EstablishedYearFormatted { get; set; }
    }
}
