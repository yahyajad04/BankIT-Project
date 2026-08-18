namespace BankingSystem.Helpers
{
    public class QueryObject
    {
        public string? Search { get; set; } = null;
        public string? SortOrder { get; set; } = null;
        public bool isAscending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
}
