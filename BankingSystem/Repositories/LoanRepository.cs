using BankingSystem.Data;
using BankingSystem.DTO_s.LoanDTO_s;
using BankingSystem.DTO_s.TransactionDTO_s;
using BankingSystem.Interfaces;
using BankingSystem.Mappers;
using BankingSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Security.Principal;
using System.Transactions;

namespace BankingSystem.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly AppDbContext _context;

        public LoanRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Loan> ApproveLoan(int Id)
        {
            using var dbTransaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var loan = await _context.Loans.Include(a => a.Account)
                    .ThenInclude(p => p.Profile)
                    .ThenInclude(u => u.AppUser)
                    .Include(r => r.Transactions)
                    .FirstOrDefaultAsync(x => x.Id == Id);
                if (loan == null)
                    throw new KeyNotFoundException("This Loan Doesnt Exist");

                if (loan.Status.Equals(LoanStatus.Approved))
                    throw new InvalidOperationException("This Loan is Already Approved");

                if (loan.Status.Equals(LoanStatus.Disapproved))
                    throw new InvalidOperationException("This Loan is Already DisApproved");

                if (!loan.Account.AccountStatus.Equals(AccountStatus.Active))
                    throw new InvalidOperationException($"Account Current Status is: {loan.Account.AccountStatus}");

                var transaction = loan.Transactions
                    .FirstOrDefault(x => x.Type.Equals(Transactiontype.Loan));

                if (transaction == null)
                    throw new InvalidOperationException("Transaction Invalid Error");

                loan.Account.Balance += loan.Amount;
                transaction.Status = TransactionStatus.Committed;

                loan.Status = LoanStatus.Approved;

                await _context.SaveChangesAsync();
                await dbTransaction.CommitAsync();
                return loan;
            }
            catch(DbUpdateConcurrencyException) {
                await dbTransaction.RollbackAsync();
                throw new InvalidOperationException("Multiple Updates in the System is not allowed");
            }
            catch (Exception)
            {
                await dbTransaction.RollbackAsync();
                throw;
            }

        }

        public async Task<Loan> CreateLoan(string UserId, Loan loan)
        {
                var account = await _context.Accounts
                .Include(p => p.Profile)
                .ThenInclude(u => u.AppUser)
                .Include(l => l.Loans).Include(t => t.RecievedTransactions)
                    .FirstOrDefaultAsync(a => a.Id == loan.AccountId);

                if(account == null) 
                    throw new KeyNotFoundException("This Account Doesnt Exist");

            if (!account.AccountStatus.Equals(AccountStatus.Active))
                throw new InvalidOperationException($"Account Current Status is: {account.AccountStatus}");

            if (loan.Type.Equals(LoanType.Personal))
                    loan.InterestRate = 0.15m;

                if (loan.Type.Equals(LoanType.Car))
                    loan.InterestRate = 0.10m;

                if (loan.Type.Equals(LoanType.Buisness))
                    loan.InterestRate = 0.2m;

                var date = ((loan.LastRepay.Year - loan.CreatedAt.Year)*12)
                    + (loan.LastRepay.Month - loan.CreatedAt.Month);

            if (date <= 0)
                throw new InvalidOperationException("Wrong Date Entered");

                loan.MonthlyRepay = (loan.Amount + loan.Amount*loan.InterestRate) / date;
                loan.RemaintoPay = (loan.Amount + loan.Amount * loan.InterestRate);

                var transaction = new Transactions
                {
                    Amount = loan.Amount,
                    ToAccountId = loan.AccountId,
                    Loan = loan,
                    Type = Transactiontype.Loan,
                    PerformedById = UserId
                };

                account.RecievedTransactions.Add(transaction);
                _context.Transactions.Add(transaction);
                _context.Loans.Add(loan);
                account.Loans.Add(loan);
                await _context.SaveChangesAsync();

                return loan;  
        }

        public async Task<Loan> DisApproveLoan(int Id)
        {
                var loan = await _context.Loans.Include(a => a.Account)
                .ThenInclude(p => p.Profile)
                .ThenInclude(u => u.AppUser)
                .Include(t => t.Transactions)
                    .FirstOrDefaultAsync(x => x.Id == Id);

                if (loan == null)
                    throw new KeyNotFoundException("This Loan Doesnt Exist");

                if (loan.Status.Equals(LoanStatus.Approved))
                    throw new InvalidOperationException("This Loan is Already Approved");

                if (loan.Status.Equals(LoanStatus.Disapproved))
                    throw new InvalidOperationException("This Loan is Already DisApproved");

                var transaction = loan.Transactions.FirstOrDefault(x => x.Type.Equals(Transactiontype.Loan));

                if (transaction == null)
                    throw new InvalidOperationException("Transaction Error");

                transaction.Status = TransactionStatus.Aborted;

                loan.Status = LoanStatus.Disapproved;

                await _context.SaveChangesAsync();
                return loan;
        }

        public async Task<List<Loan>> GetAll(int ProfileId, int AccountId)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == AccountId);
            if (account == null) 
                throw new KeyNotFoundException("Account is not Found");

            if (ProfileId != -1 && account.CustomerProfileId != ProfileId)
                throw new InvalidOperationException("You are not Authorized to See All the Loans");

            return await _context.Loans.Include(l => l.Account).Where(a => a.AccountId == AccountId)
                .ToListAsync();
        }

        public async Task<List<Loan>> GetAllLoans(int ProfileId)
        {
            if (ProfileId == -1)
                throw new InvalidOperationException("This Endpoint is Only for the Customers");

            return await _context.Loans.Include(l => l.Account)
                .Where(a => a.Account.CustomerProfileId == ProfileId)
                .ToListAsync();
        }

        public async Task<Loan> GetById(int Id)
        {
            return await _context.Loans.Include(a => a.Account).FirstOrDefaultAsync(l => l.Id == Id);
        }

        public async Task<String[]> GetLoanTypes()
        {
            return Enum.GetNames<LoanType>();
        }

        public async Task<Loan> MonthlyPayLoan(string UserId, int Id)
        {
            using var dbTransaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var loan = await _context.Loans.Include(r => r.Transactions)
                    .Include(a => a.Account)
                    .ThenInclude(p => p.Profile)
                    .ThenInclude(u => u.AppUser)
                    .Include(a => a.Account)
                    .ThenInclude(t => t.SentTransactions)
                    .FirstOrDefaultAsync(l => l.Id == Id);

                if (loan == null)
                    throw new KeyNotFoundException("Loan is Not Found");

                if (!loan.Status.Equals(LoanStatus.Approved))
                    throw new InvalidOperationException("This Loan is not Approved!");

                if (!loan.Account.AccountStatus.Equals(AccountStatus.Active))
                    throw new InvalidOperationException($"Account Current Status is: {loan.Account.AccountStatus}");

                if (loan.RemaintoPay <= 0)
                    throw new InvalidOperationException("Nothing is Remained to Pay");

                var transactionAmount = 0m;
                var balance = loan.Account.Balance;

                if (balance < loan.RemaintoPay && balance < loan.MonthlyRepay)
                    throw new InvalidOperationException("Your Balance is not enough for this operation");

                    if (loan.RemaintoPay < loan.MonthlyRepay && balance >= loan.RemaintoPay)
                {
                    loan.Account.Balance -= loan.RemaintoPay;
                    transactionAmount = loan.RemaintoPay;
                    loan.RemaintoPay = 0;
                }

                if (loan.RemaintoPay >= loan.MonthlyRepay && balance >= loan.MonthlyRepay)
                {
                    loan.Account.Balance -= loan.MonthlyRepay;
                    loan.RemaintoPay -= loan.MonthlyRepay;
                    transactionAmount = loan.MonthlyRepay;
                }

                var transaction = new Transactions
                {
                   FromAccountId = loan.Account.Id,
                   LoanId = loan.Id,
                   Type = Transactiontype.Withdraw,
                   Amount = transactionAmount,
                   PerformedById = UserId,
                   Status = TransactionStatus.Committed,
                };
                
                loan.Account.SentTransactions.Add(transaction);
                loan.Transactions.Add(transaction);

                await _context.SaveChangesAsync();
                await dbTransaction.CommitAsync();
                return loan;
            }
            catch (DbUpdateConcurrencyException)
            {
                await dbTransaction.RollbackAsync();
                throw new InvalidOperationException("Multiple Updates in the System is not allowed");
            }
            catch (Exception)
            {
                await dbTransaction.RollbackAsync();
                throw;
            }
        }
    }
}
