using HealthInsuranceV3.Models;

namespace HealthInsuranceV3.Areas.User.Services.UserInsuranceManagerService
{
    public interface IUserInsuranceManagerService
    {
        IEnumerable<UserInsuranceManagerModel> GetUserInsuranceRegistrations(string Id);
        void DeleteUserInsuranceRegistration(string Id, int RegistrationId);
    }
}
