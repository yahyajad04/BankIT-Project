using BankingSystem.Data;
using BankingSystem.Helpers;
using BankingSystem.Interfaces;
using BankingSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Frozen;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Transactions;

namespace BankingSystem.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;
        public TransactionRepository(AppDbContext context) {
            _context = context;
        }

        public async Task<Transactions> AbortTransaction(int Id)
        {
            using var dbTransaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var transaction = await _context.Transactions
                    .Include(f => f.FromAccount).ThenInclude(c => c.Profile)
                    .Include(t => t.ToAccount).ThenInclude(c => c.Profile)
                    .Include(p => p.PerformedBy).FirstOrDefaultAsync(t => t.Id == Id);

                if (transaction == null)
                    throw new KeyNotFoundException("Transaction not found.");

                if (!transaction.Status.Equals(TransactionStatus.Active))
                    throw new InvalidOperationException("Only active transactions can be aborted.");

                transaction.Status = TransactionStatus.Aborted;
                await _context.SaveChangesAsync();
                await dbTransaction.CommitAsync();

                return transaction;
            }
            catch(DbUpdateConcurrencyException)
            {
                await dbTransaction.RollbackAsync();
                throw new InvalidOperationException("Concurrency conflict occurred.");
            }
            catch (Exception)
            {
                await dbTransaction.RollbackAsync();
                throw;
            }

        }

        public async Task<Transactions> CommitTransaction(int Id)
        {
            using var dbTransaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var transaction = await _context.Transactions
                    .Include(f => f.FromAccount).ThenInclude(c => c.Profile)
                    .Include(t => t.ToAccount).ThenInclude(c => c.Profile)
                    .Include(p => p.PerformedBy)
                    .FirstOrDefaultAsync(t => t.Id == Id);

                if (transaction == null)
                    throw new KeyNotFoundException("This Transaction is Not Found");

                if(transaction.Type.Equals(Transactiontype.Loan))
                    throw new InvalidOperationException($"This is a Loan, it is Commited in the Loans Page");

                var toAccount = await _context.Accounts
                    .FirstOrDefaultAsync(t => transaction.ToAccountId == t.Id);
                var fromAccount = await _context.Accounts
                    .FirstOrDefaultAsync(t => transaction.FromAccountId == t.Id);

                if (toAccount != null && !toAccount.AccountStatus.Equals(AccountStatus.Active))
                    throw new InvalidOperationException($"The Current ToAccount Status is : {toAccount.AccountStatus}");
                if (fromAccount != null && !fromAccount.AccountStatus.Equals(AccountStatus.Active) )
                    throw new InvalidOperationException($"The Current FromAccount Status is : {fromAccount.AccountStatus}");

                if (fromAccount != null && fromAccount.Balance < transaction.Amount)
                    throw new InvalidOperationException("The Balance is Not enough!");

                if (!transaction.Status.Equals(TransactionStatus.Active))
                    throw new InvalidOperationException($"Transaction status is: {transaction.Status}ed");

                else if (transaction.Type.Equals(Transactiontype.Withdraw) && fromAccount != null)
                    fromAccount.Balance -= transaction.Amount;

                else if(transaction.Type.Equals(Transactiontype.Deposit) && toAccount != null)
                    toAccount.Balance += transaction.Amount;
                
                else if (transaction.Type.Equals(Transactiontype.Transfer) && fromAccount != null && toAccount != null)
                {
                    fromAccount.Balance -= transaction.Amount;
                    toAccount.Balance += transaction.Amount;
                }
                else
                    throw new KeyNotFoundException($"One of the Accounts is not found");

                transaction.Status = TransactionStatus.Committed;
                await _context.SaveChangesAsync();
                await dbTransaction.CommitAsync();

                return transaction;
            }
            catch(DbUpdateConcurrencyException) 
            {
                await dbTransaction.RollbackAsync();
                throw new InvalidOperationException("Two Updates at the same time is not valid");
            }
            catch(Exception) 
            {
                await dbTransaction.RollbackAsync();
                throw;
            }

        }

        public async Task<Transactions> CreateDepositTransaction(Transactions transaction)
        {
            using var dbTransaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var toAccount = await _context.Accounts
                    .Include(p => p.Profile).ThenInclude(a => a.AppUser)
                    .FirstOrDefaultAsync(t => transaction.ToAccountId == t.Id);

                if (toAccount == null)
                    throw new KeyNotFoundException("Account Doesnt Exist");

                if (!toAccount.AccountStatus.Equals(AccountStatus.Active))
                    throw new InvalidOperationException($"The Current Account Status is : {toAccount.AccountStatus}");

                transaction.Type = Transactiontype.Deposit;
                _context.Transactions.Add(transaction);

                transaction.ToAccount = toAccount;
                toAccount.RecievedTransactions.Add(transaction);

                await _context.SaveChangesAsync();
                await dbTransaction.CommitAsync();

                return transaction;
            }
            catch (DbUpdateConcurrencyException)
            {
                await dbTransaction.RollbackAsync();
                throw new InvalidOperationException("Two Updates at the same time is not valid");
            }
            catch (Exception)
            {
                await dbTransaction.RollbackAsync();
                throw;
            }
        }

        public async Task<Transactions> CreateTransferTransaction(Transactions transaction)
        {
            using var dbTransaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var fromAccount = await _context.Accounts.Include(p => p.Profile).ThenInclude(a => a.AppUser)
                    .FirstOrDefaultAsync(t => transaction.FromAccountId == t.Id);
                var toAccount = await _context.Accounts.Include(p => p.Profile).ThenInclude(a => a.AppUser)
                   .FirstOrDefaultAsync(t => transaction.ToAccountId == t.Id);

                if (fromAccount == null || toAccount == null)
                    throw new KeyNotFoundException("Account Doesnt Exist");

                if (!toAccount.AccountStatus.Equals(AccountStatus.Active))
                    throw new InvalidOperationException($"The Current ToAccount Status is : {toAccount.AccountStatus}");
                if (!fromAccount.AccountStatus.Equals(AccountStatus.Active))
                    throw new InvalidOperationException($"The Current FromAccount Status is : {fromAccount.AccountStatus}");

                if (fromAccount.Balance < transaction.Amount)
                    throw new InvalidOperationException("Your Balance is not Enough for this Transaction!");

                transaction.Type = Transactiontype.Transfer;
                _context.Transactions.Add(transaction);

                transaction.ToAccount = toAccount;
                transaction.FromAccount = fromAccount;

                fromAccount.SentTransactions.Add(transaction);
                toAccount.RecievedTransactions.Add(transaction);

                await _context.SaveChangesAsync();
                await dbTransaction.CommitAsync();
                return transaction;
            }
            catch (DbUpdateConcurrencyException)
            {
                await dbTransaction.RollbackAsync();
                throw new InvalidOperationException("Two Updates at the same time is not valid");
            }
            catch (Exception)
            {
                await dbTransaction.RollbackAsync();
                throw;
            }

        }

        public async Task<Transactions> CreateWithdrawTransaction(Transactions transaction)
        {
            using var dbTransaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var fromAccount = await _context.Accounts.Include(p => p.Profile).ThenInclude(a => a.AppUser)
                    .FirstOrDefaultAsync(t => transaction.FromAccountId == t.Id);

                if (fromAccount == null)
                    throw new KeyNotFoundException("Account Doesnt Exist");

                if (!fromAccount.AccountStatus.Equals(AccountStatus.Active))
                    throw new InvalidOperationException($"The Current FromAccount Status is : {fromAccount.AccountStatus}");

                if (fromAccount.Balance < transaction.Amount)
                    throw new InvalidOperationException("Your Balance is not Enough for this Transaction!");

                transaction.Type = Transactiontype.Withdraw;
                _context.Transactions.Add(transaction);

                transaction.FromAccount = fromAccount;

                fromAccount.SentTransactions.Add(transaction);

                await _context.SaveChangesAsync();
                await dbTransaction.CommitAsync();
                return transaction;
            }
            catch (DbUpdateConcurrencyException)
            {
                await dbTransaction.RollbackAsync();
                throw new InvalidOperationException("Two Updates at the same time is not valid");
            }
            catch (Exception)
            {
                await dbTransaction.RollbackAsync();
                throw;
            }
        }

        public async Task<Transactions> DeleteTransaction(int Id)
        {
            try
            {
                var transaction = await _context.Transactions.Include(p => p.PerformedBy)
                    .FirstOrDefaultAsync(t => t.Id == Id);
                if (transaction == null)
                    throw new KeyNotFoundException("This Transaction Doesnt Exist");

                if (!transaction.Status.Equals(TransactionStatus.Aborted))
                    throw new InvalidOperationException($"Transaction status: {transaction.Status}");
                
                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();
                return transaction;
            }
            catch(Exception) 
            {
                throw;
            }

        }

        public async Task<List<Transactions>> GetAll(QueryObject query, int profileId)
        {
            try
            {
                if (profileId != -1)
                {
                    var transactions = _context.Transactions.Include(f => f.FromAccount).ThenInclude(c => c.Profile).ThenInclude(a => a.AppUser)
                       .Include(t => t.ToAccount).ThenInclude(c => c.Profile).ThenInclude(a => a.AppUser)
                       .Include(p => p.PerformedBy)
                       .Where(x => (x.FromAccount != null && x.FromAccount.Profile.Id == profileId) || (x.ToAccount !=null && x.ToAccount.Profile.Id == profileId)).AsQueryable();

                    if (!query.Search.IsNullOrEmpty())
                    {
                        transactions = transactions.Where(t =>
                            t.PerformedBy.Name.Contains(query.Search) ||
                            t.PerformedBy.Email.Contains(query.Search) ||
                            t.FromAccount.AccountNumber.ToString().Contains(query.Search) ||
                            t.ToAccount.AccountNumber.ToString().Contains(query.Search)
                            );
                    }

                    if (!query.SortOrder.IsNullOrEmpty())
                    {
                        switch (query.SortOrder)
                        {
                            case "date":
                                transactions = query.isAscending ? transactions.OrderBy(t => t.CreatedAt) :
                                    transactions.OrderByDescending(t => t.CreatedAt); break;
                            case "amount":
                                transactions = query.isAscending ? transactions.OrderBy(t => t.Amount) :
                                    transactions.OrderByDescending(t => t.Amount); break;
                            case "status":
                                transactions = query.isAscending ? transactions.OrderBy(t => t.Status) :
                                    transactions.OrderByDescending(t => t.Status); break;
                        }
                    }
                    transactions = query.isAscending ? transactions.OrderBy(t => t.CreatedAt) :
                                    transactions.OrderByDescending(t => t.CreatedAt);
                    var skipnum = (query.PageNumber - 1) * query.PageSize;
                    return await transactions.Skip(skipnum).Take(query.PageSize).ToListAsync();
                } else
                    throw new Exception("You are not a Customer");
            }
            catch (Exception e)
            {
                throw;
            }
           
        }
        public async Task<List<Transactions>> GetAllTransactions(QueryObject query, int Id, int profileId)
        {
            var transactions = _context.Transactions.Include(f => f.FromAccount).ThenInclude(c =>c.Profile).ThenInclude(a => a.AppUser)
                .Include(t => t.ToAccount).ThenInclude(c => c.Profile).ThenInclude(a => a.AppUser)
                .Include(p => p.PerformedBy)
                .Where(x => (x.FromAccountId == Id || x.ToAccountId == Id)).AsQueryable();

            if(profileId != -1)
                transactions = transactions.Where(p => (p.FromAccount != null && p.FromAccount.Profile.Id == profileId) ||
               (p.ToAccount != null && p.ToAccount.Profile.Id == profileId));

            if (!query.Search.IsNullOrEmpty())
            {
                transactions = transactions.Where(t =>
                    t.PerformedBy.Name.Contains(query.Search) ||
                    t.PerformedBy.Email.Contains(query.Search) ||
                    t.FromAccount.AccountNumber.ToString().Contains(query.Search) ||
                    t.ToAccount.AccountNumber.ToString().Contains(query.Search) 
                    );
            }

            if(!query.SortOrder.IsNullOrEmpty())
            {
                switch (query.SortOrder)
                {
                    case "date":
                        transactions = query.isAscending ? transactions.OrderBy(t => t.CreatedAt) :
                            transactions.OrderByDescending(t => t.CreatedAt); break;
                    case "amount":
                        transactions = query.isAscending ? transactions.OrderBy(t => t.Amount) :
                            transactions.OrderByDescending(t => t.Amount); break;
                    case "status":
                        transactions = query.isAscending ? transactions.OrderBy(t => t.Status) :
                            transactions.OrderByDescending(t => t.Status); break;
                }
            }
            var skipnum = (query.PageNumber - 1) * query.PageSize;
            return await transactions.Skip(skipnum).Take(query.PageSize).ToListAsync();
        }

        public async Task<Transactions> GetTransactionById(int Id)
        {
            return await _context.Transactions
                .Include(f => f.FromAccount).ThenInclude(c => c.Profile).ThenInclude(a => a.AppUser)
                .Include(t => t.ToAccount).ThenInclude(c => c.Profile).ThenInclude(a => a.AppUser)
                .Include(p => p.PerformedBy)
                .FirstOrDefaultAsync(t => t.Id == Id);
        }
    }
}
