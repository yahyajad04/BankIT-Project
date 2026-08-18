using BankingSystem.Helpers;
using BankingSystem.Models;

namespace BankingSystem.Interfaces
{
    public interface ITransactionRepository
    {
        Task<List<Transactions>> GetAll(QueryObject query, int profileId);
        Task<List<Transactions>> GetAllTransactions(QueryObject query, int Id, int profileId);
        Task<Transactions> GetTransactionById(int Id);
        Task<Transactions> CreateTransferTransaction(Transactions transaction);
        Task<Transactions> CreateDepositTransaction(Transactions transaction);
        Task<Transactions> CreateWithdrawTransaction(Transactions transaction);
        Task<Transactions> DeleteTransaction(int Id);
        Task<Transactions> AbortTransaction(int Id);
        Task<Transactions> CommitTransaction(int Id);
    }
}
