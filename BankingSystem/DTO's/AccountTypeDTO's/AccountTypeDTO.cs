namespace BankingSystem.DTO_s.AccountTypeDTO_s
{
    public class AccountTypeDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool isUsable { get; set; } = false;
    }
}
