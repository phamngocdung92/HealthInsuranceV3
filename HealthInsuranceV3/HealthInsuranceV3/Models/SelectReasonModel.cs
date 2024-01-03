namespace HealthInsuranceV3.Models
{
    public class SelectReasonModel
    {
        public string UserId { get; set; }
        public int RegistrationId { get; set; }
        public IEnumerable<ForManagerModel> Data { get; set; }
    }
}
