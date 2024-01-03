using HealthInsuranceV3.Models;

namespace HealthInsuranceV3.Areas.User.Services.ListInsuranceService
{
    public interface IListInsuranceService
    {
        IEnumerable<ListInsuranceModel> GetInsuranceList();
    }
}
