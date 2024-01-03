namespace HealthInsuranceV3.Models
{
	public class InsuranceDetailModel
	{
		public string PackageName { get; set; } = null!;
		public string? CoverageDetails { get; set; }
		public int? PolicyTermInDay { get; set; }
		public decimal? Price { get; set; }
		public string CompanyName { get; set; } = null!;
		public string? ContactInfo { get; set; }

		public string? Address { get; set; }

		public DateTime? EstablishedYear { get; set; }
		public string EstablishedYearFormatted { get; set; }

        public int PackageId { get; set; }

		public int? InsuranceId { get; set; }
		public string? IconText { get; set; }
	}
}