namespace ExpenseTrackerApi_04.Dtos.Expense
{
    public class ExpensesByCategory
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int TotalExpenses { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
