namespace ExpenseTrackerApi_04.Domain
{
    public class Category
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        private Category()
        {
            Name = string.Empty;
            Description = string.Empty;
        }
        public Category(string name, string description)
        {
            validate(name, description);

            Id = Guid.NewGuid();
            Name = name;
            Description = description;
        }
        public void Update(string name, string description)
        {
            validate(name, description);
            Name = name;
            Description = description;
        }
        private void validate(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Description cannot be null or empty.", nameof(description));
            }
        }
    }
}
