using HealthInsuranceV3.Areas.User.Repositories.InsuranceDetailRepository;
using HealthInsuranceV3.Models;

namespace HealthInsuranceV3.Areas.User.Services.InsuranceDetailService
{
	public class InsuranceDetailService : IInsuranceDetailService
	{
		private readonly IInsuranceDetailRepository _InsuranceDetailRepository;

		public InsuranceDetailService(IInsuranceDetailRepository InsuranceDetailRepository)
		{
			_InsuranceDetailRepository = InsuranceDetailRepository;
		}

		public IEnumerable<InsuranceDetailModel> GetInsuranceDetail(int InsuranceId)
		{
			return _InsuranceDetailRepository.GetInsuranceDetail(InsuranceId);
		}
		public void RegisterInsuranceForEmployee(string UserId, int PackageId)
		{
			// Call the repository to register the insurance
			_InsuranceDetailRepository.RegisterInsuranceForEmployee(UserId, PackageId);
		}
        public IEnumerable<InsuranceDetailModel> GetPackageDetail(int PackageId)
        {
            return _InsuranceDetailRepository.GetPackageDetail(PackageId);
        }
    }
}
