using HealthInsuranceV3.Models;

namespace HealthInsuranceV3.Areas.User.Services.InsuranceDetailService
{
	public interface IInsuranceDetailService
	{
		IEnumerable<InsuranceDetailModel> GetInsuranceDetail(int InsuranceID);
		void RegisterInsuranceForEmployee(string UserId, int PackageId);
        IEnumerable<InsuranceDetailModel> GetPackageDetail(int PackageId);
    }
}
