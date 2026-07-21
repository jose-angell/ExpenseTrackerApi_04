using ExpenseTrackerApi_04.Infrastructure;

namespace ExpenseTrackerApi_04.Application
{
    public class CategoryUseCase
    {
        private readonly AppDbContext _context;
        public CategoryUseCase(AppDbContext context)
        {
            _context = context;
        }
         
    }
}
