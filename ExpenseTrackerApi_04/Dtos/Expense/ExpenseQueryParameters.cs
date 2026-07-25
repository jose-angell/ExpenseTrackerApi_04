namespace ExpenseTrackerApi_04.Dtos.Expense
{
    public class ExpenseQueryParameters
    {
        public Guid? CategoryId { get; set; }
        public DateTimeOffset? FromDate { get; set; }
        public DateTimeOffset? ToDate { get; set; }
        public string? Search { get; set; }
        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }
        public string? Sort { get; set; }
        public int? Page { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
    }
}
