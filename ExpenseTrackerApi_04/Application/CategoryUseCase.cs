using ExpenseTrackerApi_04.Domain;
using ExpenseTrackerApi_04.Domain.Exceptions;
using ExpenseTrackerApi_04.Dtos.Category;
using ExpenseTrackerApi_04.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerApi_04.Application
{
    public class CategoryUseCase
    {
        private readonly AppDbContext _context;
        public CategoryUseCase(AppDbContext context)
        {
            _context = context;
        }
        public async Task<CategoryDto> Create(CreateCategory request)
        {
            var existCategory = await _context.Categories.AnyAsync(c => c.Name == request.Name);
            if (existCategory) throw new ConflictException("Ya existe una categoria con el mismo nombre");
            var newCategory = new Category(request.Name, request.Description);
            await _context.Categories.AddAsync(newCategory);
            return MapToDto(newCategory);
        }
        public async Task Update(Guid id, UpdateCategory request)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) throw new NotFoundException("No existe la categoria");
            category.Update(request.Name, request.Description);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) throw new NotFoundException("Categoria no encontrada");
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
        public async Task<CategoryDto> GetById(Guid id)
        {
            var category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            if (category == null) throw new NotFoundException("Categoria no encontrada");
            return MapToDto(category);
        }
        public async Task<IEnumerable<CategoryDto>> GetAll()
        {
            var categories = await _context.Categories.AsNoTracking().ToListAsync();
            return categories.Select(MapToDto);
        }
        private static CategoryDto MapToDto(Category category) => new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };
    }
}
