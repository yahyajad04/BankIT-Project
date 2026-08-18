using BankingSystem.DTO_s.CustomerProfileDTO_s;
using BankingSystem.Helpers;
using BankingSystem.Models;

namespace BankingSystem.Interfaces
{
    public interface ICustomerProfileRepository
    {
        Task<CustomerProfile> CreateProfile(CustomerProfile profile);
        Task<List<CustomerProfile>> GetAll(QueryObject query);
        Task<CustomerProfile> EditProfile(int Id, EditCustomerProfileDTO profiledto);
        Task<CustomerProfile> EditCustomerProfile(int Id, EditProfileDTO profiledto);
    }
}
