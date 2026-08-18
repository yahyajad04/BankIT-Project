using BankingSystem.Models;

namespace BankingSystem.Interfaces
{
    public interface IEmployeeProfileRepository
    {
        Task<EmployeeProfile> CreateProfile(EmployeeProfile employeeProfile);
    }
}
