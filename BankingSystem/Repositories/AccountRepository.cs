using BankingSystem.Data;
using BankingSystem.DTO_s.Account;
using BankingSystem.Helpers;
using BankingSystem.Interfaces;
using BankingSystem.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BankingSystem.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;
        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Account> CreateAccount(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<Account> CloseAccount(int accountId)
        {
            var account = _context.Accounts.Include(l => l.Loans).Include(p => p.Profile).ThenInclude(a => a.AppUser)
                .FirstOrDefault(a => a.Id == accountId);
            if (account == null)
                throw new KeyNotFoundException("This Account Doesnt Exist");

            if(account.Loans != null)
            {
                if (account.Loans.Any(l => l.RemaintoPay > 0))
                    throw new InvalidOperationException("There are Loans that Are not Payed on this Account");
            }

            account.AccountStatus = AccountStatus.Closed;
            await _context.SaveChangesAsync();

            return account;
        }

        public async Task<Account> FreezeAccount(int accountId)
        {
            var account = _context.Accounts.Include(p => p.Profile).ThenInclude(a => a.AppUser)
                .FirstOrDefault(a => a.Id == accountId);
            if (account == null)
                throw new KeyNotFoundException("This Account Doesnt Exist");

            account.AccountStatus = AccountStatus.Frozen;
            await _context.SaveChangesAsync();

            return account;
        }

        public async Task<Account> DeleteAccount(int accountId)
        {
            var account = await _context.Accounts.Include(p => p.Profile).ThenInclude(u => u.AppUser)
                .Include(l => l.Loans)
                .FirstOrDefaultAsync(a => a.Id == accountId);

            if (account == null)
                throw new KeyNotFoundException("This Account Doesnt Exist");

            if (account.Loans != null)
            {
                if (account.Loans.Any(l => l.RemaintoPay > 0))
                    throw new InvalidOperationException("There are Loans that Are not Payed on this Account");
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return account;
        }

        public async Task<Account> EditAccount(int accountId, EditAccountDTO accountdto)
        {
            var account = await _context.Accounts.Include(t => t.AccountType)
                .Include(c => c.Profile)
                .ThenInclude(u => u.AppUser)
                .FirstOrDefaultAsync(a => a.Id == accountId);

            if (account == null)
                throw new KeyNotFoundException("This Account Doesnt Exist");

            if(accountdto.AccountTypeId != null) {
                var accountType = await _context.AccountTypes.FindAsync(accountdto.AccountTypeId);
                if (accountType == null)
                    throw new KeyNotFoundException("This Account Type Doesnt Exist");
            }

            account.AccountTypeId = accountdto.AccountTypeId ?? account.AccountTypeId;
            account.Balance = accountdto.Balance ?? account.Balance;

            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<List<Account>> GetAll(QueryObject query, int ProfileId)
        {
            if(ProfileId != -1)
            {
                var accounts =   _context.Accounts.Include(t => t.AccountType)
                    .Include(c => c.Profile).ThenInclude(a => a.AppUser)
                    .AsQueryable();

                if (accounts == null)
                    throw new KeyNotFoundException("There Are no Accounts");

                if (ProfileId != -1)
                    accounts = accounts.Where(x => x.CustomerProfileId == ProfileId);

                if (!query.Search.IsNullOrEmpty())
                {
                    accounts = accounts.Where(a => 
                    a.AccountType.Name.Contains(query.Search)||
                    a.AccountNumber.ToString().Contains(query.Search) 
                    );
                }
                if (!query.SortOrder.IsNullOrEmpty())
                {
                    switch (query.SortOrder.ToLower().Replace(" ", "").Trim())
                    {
                        case "balance": 
                            accounts = query.isAscending ? accounts.OrderBy(a => a.Balance) :
                                accounts.OrderByDescending(a => a.Balance);
                            break;
                        case "date":
                            accounts = query.isAscending ? accounts.OrderBy(a => a.CreatedAt) :
                                accounts.OrderByDescending(a => a.CreatedAt);
                            break;
                        case "status":
                            accounts = query.isAscending ? accounts.OrderBy(a => a.AccountStatus) :
                               accounts.OrderByDescending(a => a.AccountStatus);
                            break;
                    }
                }
                var skipnum = (query.PageNumber -  1) * query.PageSize;
                return await accounts.Skip(skipnum).Take(query.PageSize).ToListAsync();
            }
            else
            {
                var accounts = _context.Accounts.Include(t => t.AccountType)
                        .Include(c => c.Profile).ThenInclude(a => a.AppUser)
                        .AsQueryable();

                if (accounts == null)
                    throw new KeyNotFoundException("There Are no Accounts");

                if (!query.Search.IsNullOrEmpty())
                {
                    accounts = accounts.Where(a =>
                    a.AccountType.Name.Contains(query.Search) ||
                    a.AccountNumber.ToString().Contains(query.Search) ||
                    a.Profile.NationalId.ToString().Contains(query.Search)
                    );
                }
                if (!query.SortOrder.IsNullOrEmpty())
                {
                    switch (query.SortOrder.ToLower().Replace(" ", "").Trim())
                    {
                        case "balance":
                            accounts = query.isAscending ? accounts.OrderBy(a => a.Balance) :
                                accounts.OrderByDescending(a => a.Balance);
                            break;
                        case "date":
                            accounts = query.isAscending ? accounts.OrderBy(a => a.CreatedAt) :
                                accounts.OrderByDescending(a => a.CreatedAt);
                            break;
                        case "status":
                            accounts = query.isAscending ? accounts.OrderBy(a => a.AccountStatus) :
                               accounts.OrderByDescending(a => a.AccountStatus);
                            break;
                    }
                }
                var skipnum = (query.PageNumber - 1) * query.PageSize;
                return await accounts.Skip(skipnum).Take(query.PageSize).ToListAsync();
            }
        }

        public async Task<Account> ReActivateAccount(int accountId)
        {
            var account = await _context.Accounts.Include(c =>c.Profile).ThenInclude(a => a.AppUser)
                .FirstOrDefaultAsync(x => x.Id == accountId);

            if (account == null)
                throw new KeyNotFoundException("This Account Doesnt Exist");

            if (account.AccountStatus.Equals(AccountStatus.Closed))
                throw new InvalidOperationException("This Account is closed and cant be reactivated");

            account.AccountStatus = AccountStatus.Active;
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<Account> GetById(int Id)
        {
            return await _context.Accounts.Include(t => t.AccountType)
                    .Include(c => c.Profile).ThenInclude(a => a.AppUser)
                    .FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Account> GetByNumber(int AccountNumber)
        {
            return await _context.Accounts.Include(t => t.AccountType)
                   .Include(c => c.Profile).ThenInclude(a => a.AppUser)
                   .FirstOrDefaultAsync(x => x.AccountNumber == AccountNumber);
        }
    }
}
