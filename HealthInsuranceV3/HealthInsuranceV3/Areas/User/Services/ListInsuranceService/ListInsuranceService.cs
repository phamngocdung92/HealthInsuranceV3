using HealthInsuranceV3.Areas.User.Repositories.ListInsuranceRepository;
using HealthInsuranceV3.Models;

namespace HealthInsuranceV3.Areas.User.Services.ListInsuranceService
{
    public class ListInsuranceService: IListInsuranceService
    {
        private readonly IListInsuranceRepository _ListInsuranceRepository;
        public ListInsuranceService(IListInsuranceRepository ListInsuranceRepository)
        {
            _ListInsuranceRepository = ListInsuranceRepository;
        }
        public IEnumerable<ListInsuranceModel> GetInsuranceList()
        {
            return _ListInsuranceRepository.GetInsuranceList();
        }
    }
}
