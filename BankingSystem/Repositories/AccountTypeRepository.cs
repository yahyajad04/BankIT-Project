using BankingSystem.Data;
using BankingSystem.DTO_s.AccountTypeDTO_s;
using BankingSystem.Interfaces;
using BankingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Repositories
{
    public class AccountTypeRepository : IAccountTypeRepository
    {
        private readonly AppDbContext _context;
        public AccountTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AccountType> CreateType(AccountType type)
        {
            var exist_type = await _context.AccountTypes.FirstOrDefaultAsync(t => t.Name.Equals(type.Name));
            if (exist_type != null)
                throw new InvalidOperationException("This Type Name Already Exists");
            _context.AccountTypes.Add(type);
            await _context.SaveChangesAsync();
            return type;
        }

        public async Task<AccountType> DeleteType(int Id)
        {
            var type = await _context.AccountTypes.FindAsync(Id);
            if (type == null)
                throw new KeyNotFoundException("This Account Type Doesnt Exist");
            var accountexist = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountTypeId == type.Id);
            if (accountexist != null)
                throw new InvalidOperationException("There is an Account Registered with this Type, You have to Delete the Account First");

            _context.AccountTypes.Remove(type);
            await _context.SaveChangesAsync();
            return type;
        }

        public async Task<AccountType> EditType(int Id, EditAccountTypeDTO typedto)
        {
            var type = await _context.AccountTypes.FindAsync(Id);
            if (type == null)
                throw new KeyNotFoundException("This Account Type Doesnt Exist");
            type.Name = typedto.Name ?? type.Name;
            type.Description = typedto.Description ?? type.Description;

            await _context.SaveChangesAsync();
            return type;
        }

        public async Task<List<AccountType>> GetAll()
        {
            return await _context.AccountTypes.ToListAsync();
        }
    }
}
