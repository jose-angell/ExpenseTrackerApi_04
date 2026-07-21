namespace ExpenseTrackerApi_04.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string message, string v) : base(message) { }
    }
}
