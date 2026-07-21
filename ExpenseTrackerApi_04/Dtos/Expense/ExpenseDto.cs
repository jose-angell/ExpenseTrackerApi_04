using ExpenseTrackerApi_04.Dtos.Category;

namespace ExpenseTrackerApi_04.Dtos.Expense
{
    public class ExpenseDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset ExpenseDate { get; set; }
        public CategoryDto? Category { get; set; }
    }
}
