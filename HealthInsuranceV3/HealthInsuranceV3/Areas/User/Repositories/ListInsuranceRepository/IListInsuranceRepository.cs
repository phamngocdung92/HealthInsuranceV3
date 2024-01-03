using HealthInsuranceV3.Models;

namespace HealthInsuranceV3.Areas.User.Repositories.ListInsuranceRepository
{
    public interface IListInsuranceRepository
    {
        IEnumerable<ListInsuranceModel> GetInsuranceList();
    }
}
