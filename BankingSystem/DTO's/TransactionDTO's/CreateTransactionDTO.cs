using BankingSystem.DTO_s.Account;
using BankingSystem.Models;
using System.Text.Json.Serialization;
using System.Transactions;

namespace BankingSystem.DTO_s.TransactionDTO_s
{
    public class CreateTransactionDTO
    {
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public decimal Amount { get; set; }
        [JsonIgnore]
        public Transactiontype? Type { get; set; }
        [JsonIgnore]
        public string? PerformedById { get; set; }
    }
}
