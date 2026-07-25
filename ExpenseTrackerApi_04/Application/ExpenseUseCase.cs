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
            var existCategory = await _context.Categories.AnyAsync(c => c.Id == request.CategoryId);
            if (!existCategory) throw new ConflictException("La categoria no existe en el sistema");
            await _context.Expenses.AddAsync(expense);
            await _context.SaveChangesAsync();
            return MapToDto(expense);
        }
        public async Task Update(Guid id,UpdateExpense request)
        {
            var expense  = await _context.Expenses.FindAsync(id);
            if (expense == null) throw new NotFoundException("Gasto no encontrado");
            expense.Update(request.Description,request.Amount,request.ExpenseDate,request.CategoryId);
            var existCategory = await _context.Categories.AnyAsync(c => c.Id == request.CategoryId);
            if (!existCategory) throw new NotFoundException("La categoria no existe en el sistema");
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
        public async Task<IEnumerable<ExpenseDto>> GetAll(ExpenseQueryParameters query)
        {
            // 1. Crear la consulta base sin ejecutarla aún en la base de datos
            var expensesQuery = _context.Expenses
                .AsNoTracking()
                .Include(e => e.Category)
                .AsQueryable();

            // 2. Filtro por Texto (Búsqueda)
            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                expensesQuery = expensesQuery.Where(e => e.Description.ToLower().Contains(query.Search.ToLower()));
            }

            // 3. Filtro por Categoría
            if (query.CategoryId.HasValue)
            {
                expensesQuery = expensesQuery.Where(e => e.CategoryId == query.CategoryId.Value);
            }

            // 4. Filtro por Rango de Montos (minAmount y maxAmount )
            var exactMin = query.MinAmount;
            if (exactMin.HasValue)
            {
                expensesQuery = expensesQuery.Where(e => e.Amount >= exactMin.Value);
            }

            var exactMax = query.MaxAmount;
            if (exactMax.HasValue)
            {
                expensesQuery = expensesQuery.Where(e => e.Amount <= exactMax.Value);
            }

            // 6. Filtro por rango de fechas
            if (query.FromDate.HasValue)
            {
                expensesQuery = expensesQuery.Where(e => e.ExpenseDate >= query.FromDate);
            }
            if (query.ToDate.HasValue)
            {
                expensesQuery = expensesQuery.Where(e => e.ExpenseDate <= query.ToDate);
            }

            // 6. Ordenamiento Dinámico (Sort)
            expensesQuery = query.Sort?.ToLower() switch
            {
                "amount_desc" => expensesQuery.OrderByDescending(e => e.Amount),
                "amount" => expensesQuery.OrderBy(e => e.Amount),
                "desc" => expensesQuery.OrderByDescending(e => e.Description), // Ejemplo por descripción
                _ => expensesQuery.OrderByDescending(e => e.ExpenseDate) // Orden por defecto (ej. ID o Fecha)
            };

            // 7. Validar valores de Paginación
            int currentPage = query.Page ?? 1;
            int currentPageSize = query.PageSize ?? 10;

            if (currentPage < 1) currentPage = 1;
            if (currentPageSize < 1 || currentPageSize > 100) currentPageSize = 10;

            // 8. Aplicar Paginación y Ejecutar Consulta en la Base de Datos
            var expenses = await expensesQuery 
                .Skip((currentPage - 1) * currentPageSize) // cantidad de registros a saltar
                .Take(currentPageSize) // numero de reguistros a tomar
                .ToListAsync(); // es aqui donde finalmente la consulta se ejecuta

            // 9. Mapear a DTO en memoria
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
