namespace ExpenseTrackerApi_04.Domain
{
    public class Expense
    {
        public Guid Id { get; private set; }
        public string Description { get; private set; }
        public decimal Amount { get; private set; }
        public DateTimeOffset ExpenseDate { get; private set; }
        public Guid CategoryId { get; private set; }
        public Category? Category { get; private set; }
        private Expense()
        {
            Description = string.Empty;
            Amount = 0;
            ExpenseDate = DateTimeOffset.UtcNow;
            CategoryId = Guid.Empty;
        }
        public Expense(string description, decimal amount, DateTimeOffset expenseDate, Guid categoryId)
        {
            validate(description, amount, expenseDate, categoryId);
            Id = Guid.NewGuid();
            Description = description;
            Amount = amount;
            ExpenseDate = expenseDate;
            CategoryId = categoryId;
        }
        public void Update(string description, decimal amount, DateTimeOffset expenseDate, Guid categoryId)
        {
            validate(description, amount, expenseDate, categoryId);
            Description = description;
            Amount = amount;
            ExpenseDate = expenseDate;
            CategoryId = categoryId;
        }
        private void validate(string description, decimal amount, DateTimeOffset expenseDate, Guid categoryId)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Description cannot be null or empty.", nameof(description));
            }

            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be a positive value.", nameof(amount));
            }

            if (categoryId == Guid.Empty)
            {
                throw new ArgumentException("CategoryId cannot be empty.", nameof(categoryId));
            }
            if (expenseDate > DateTimeOffset.UtcNow)
            {
                throw new ArgumentException("Expense date cannot be in the future.", nameof(expenseDate));
            }
        }
    }
}
