namespace HealthInsuranceV3.Models
{
    public class DepartmentAndManager
    {
        public IEnumerable<ForManagerModel> dataDepartment { get; set; }
        public IEnumerable<ForManagerModel> dataManager { get; set; }
        public IEnumerable<ForManagerModel> Data { get; set; }
    }
}
