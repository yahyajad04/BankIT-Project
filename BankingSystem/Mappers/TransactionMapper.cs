using BankingSystem.DTO_s.TransactionDTO_s;
using BankingSystem.Models;

namespace BankingSystem.Mappers
{
    public static class TransactionMapper
    {
        public static TransactionDTO toTransactionDTO(this Transactions transaction)
        {
            return new TransactionDTO
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                CreatedAt = transaction.CreatedAt,
                FromAccountId = transaction.FromAccountId,
                FromAccount = transaction.FromAccount?.toAccountDTO(),
                PerformedBy = transaction.PerformedBy?.toAppUserDTO(),
                PerformedById = transaction.PerformedById,
                Status = transaction.Status,
                ToAccountId = transaction.ToAccountId,
                ToAccount = transaction.ToAccount?.toAccountDTO(),
                Type = transaction.Type,
                LoanId = transaction.LoanId,
            };
        }

        public static Transactions fromCreateTransactionDTO(this CreateTransactionDTO createTransactionDTO)
        {
            return new Transactions
            {
                Type = createTransactionDTO.Type,
                ToAccountId = createTransactionDTO.ToAccountId,
                FromAccountId = createTransactionDTO.FromAccountId,
                Amount = createTransactionDTO.Amount,
                PerformedById = createTransactionDTO.PerformedById,
            };
        }
        public static Transactions fromCreateDepositTransactionDTO(this CreateDepositTransactionDTO createTransactionDTO)
        {
            return new Transactions
            {
                Type = createTransactionDTO.Type,
                ToAccountId = createTransactionDTO.ToAccountId,
                Amount = createTransactionDTO.Amount,
                PerformedById = createTransactionDTO.PerformedById,
            };
        }
        public static Transactions fromCreateWithdrawTransactionDTO(this CreateWithdrawTransactionDTO createTransactionDTO)
        {
            return new Transactions
            {
                Type = createTransactionDTO.Type,
                FromAccountId = createTransactionDTO.FromAccountId,
                Amount = createTransactionDTO.Amount,
                PerformedById = createTransactionDTO.PerformedById,
            };
        }
    }
}
