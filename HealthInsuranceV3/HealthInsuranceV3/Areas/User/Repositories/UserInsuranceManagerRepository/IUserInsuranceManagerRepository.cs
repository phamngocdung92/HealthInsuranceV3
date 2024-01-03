using HealthInsuranceV3.Models;

namespace HealthInsuranceV3.Areas.User.Repositories.UserInsuranceManagerRepository
{
    public interface IUserInsuranceManagerRepository
    {
        IEnumerable<UserInsuranceManagerModel> GetUserInsuranceRegistrations(string Id);
        void DeleteUserInsuranceRegistration(string Id, int RegistrationId);
    }
}
