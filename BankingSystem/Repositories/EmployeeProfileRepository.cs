using BankingSystem.Data;
using BankingSystem.Interfaces;
using BankingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Repositories
{
    public class EmployeeProfileRepository : IEmployeeProfileRepository
    {
        private readonly AppDbContext _context;
        public EmployeeProfileRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<EmployeeProfile> CreateProfile(EmployeeProfile employeeProfile)
        {
            _context.EmployeeProfiles.Add(employeeProfile);
            await _context.SaveChangesAsync();

            var employee = await _context.EmployeeProfiles
                .FirstOrDefaultAsync(e => e.AppUserId.Equals(employeeProfile.AppUserId));
            if (employee == null)
                return employeeProfile;

            return employee;
        }
    }
}
