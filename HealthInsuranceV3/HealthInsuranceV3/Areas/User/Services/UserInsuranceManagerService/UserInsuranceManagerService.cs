using HealthInsuranceV3.Areas.User.Repositories.InsuranceDetailRepository;
using HealthInsuranceV3.Areas.User.Repositories.ListInsuranceRepository;
using HealthInsuranceV3.Areas.User.Repositories.UserInsuranceManagerRepository;
using HealthInsuranceV3.Areas.User.Services.InsuranceDetailService;
using HealthInsuranceV3.Areas.User.Services.ListInsuranceService;
using HealthInsuranceV3.Models;

namespace HealthInsuranceV3.Areas.User.Services.UserInsuranceManagerService
{
    public class UserInsuranceManagerService : IUserInsuranceManagerService
    {
        private readonly IUserInsuranceManagerRepository _UserInsuranceManagerRepository;
        public UserInsuranceManagerService(IUserInsuranceManagerRepository UserInsuranceManagerRepository)
        {
            _UserInsuranceManagerRepository = UserInsuranceManagerRepository;
        }
        public IEnumerable<UserInsuranceManagerModel> GetUserInsuranceRegistrations(string Id)
        {
            return _UserInsuranceManagerRepository.GetUserInsuranceRegistrations(Id);
        }
        public void DeleteUserInsuranceRegistration(string Id, int RegistrationId)
        {
            // Call the repository to register the insurance
            _UserInsuranceManagerRepository.DeleteUserInsuranceRegistration(Id, RegistrationId);
        }
    }
}
