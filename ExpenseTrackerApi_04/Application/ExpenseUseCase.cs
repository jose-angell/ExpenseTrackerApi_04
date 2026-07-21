using ExpenseTrackerApi_04.Domain;
using ExpenseTrackerApi_04.Domain.Exceptions;
using ExpenseTrackerApi_04.Dtos.Category;
using ExpenseTrackerApi_04.Dtos.Expense;
using ExpenseTrackerApi_04.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerApi_04.Application
{
    public class ExpenseUseCase
    {
        private readonly AppDbContext _context;
        public ExpenseUseCase(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ExpenseDto> Create(CreateExpense request)
        {
            var expense = new Expense(request.Description, request.Amount, request.ExpenseDate, request.CategoryId);
            await _context.Expenses.AddAsync(expense);
            await _context.SaveChangesAsync();
            return MapToDto(expense);
        }
        public async Task Update(Guid id,UpdateExpense request)
        {
            var expense  = await _context.Expenses.FindAsync(id);
            if (expense == null) throw new NotFoundException("Gasto no encontrado");
            expense.Update(request.Description,request.Amount,request.ExpenseDate,request.CategoryId);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Guid id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null) throw new NotFoundException("Gasto no encontrado");
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
        }
        public async Task<ExpenseDto?> GetById(Guid id)
        {
            var expense = await _context.Expenses.AsNoTracking().Include(e => e.Category).FirstOrDefaultAsync(e => e.Id == id);
            if (expense == null) throw new NotFoundException("Gasto no encontrado");
            return MapToDto(expense);
        }
        public async Task<IEnumerable<ExpenseDto>> GetAll()
        {
            var expenses = await _context.Expenses.AsNoTracking().Include(e => e.Category).ToListAsync();
            return expenses.Select(MapToDto);
        }
        private static ExpenseDto MapToDto(Expense expense) => new ExpenseDto
        {
            Id = expense.Id,
            Description = expense.Description,
            Amount = expense.Amount,
            ExpenseDate = expense.ExpenseDate,
            Category = new CategoryDto
            {
                Id = expense.Category?.Id ?? Guid.Empty ,
                Name = expense.Category?.Name ?? string.Empty,
                Description = expense.Category?.Description ?? string.Empty,
            }
        };
    }
}
