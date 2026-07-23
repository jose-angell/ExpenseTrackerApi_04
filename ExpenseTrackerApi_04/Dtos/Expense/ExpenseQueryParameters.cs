namespace ExpenseTrackerApi_04.Dtos.Expense
{
    public class ExpenseQueryParameters
    {
        public Guid? CategoryId { get; set; }
        public decimal? From { get; set; }
        public decimal? To { get; set; }
        public string? Search { get; set; }
        public decimal? minAmount { get; set; }
        public decimal? maxAmount { get; set; }
        public string? sort {  get; set; }
        public int? page { get; set; } = 1;
        public int? pageSize { get; set; } = 10;
    }
}
