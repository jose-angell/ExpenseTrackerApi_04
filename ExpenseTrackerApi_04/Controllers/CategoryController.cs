using ExpenseTrackerApi_04.Application;
using ExpenseTrackerApi_04.Dtos.Category;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerApi_04.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController: ControllerBase
    {
        private readonly CategoryUseCase _useCase;
        public CategoryController(CategoryUseCase useCase)
        {
            _useCase = useCase;
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            if (id == Guid.Empty) return BadRequest("Id invalido");
            var category = await _useCase.GetById(id);
            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategory request)
        {
            var newCategory = await _useCase.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = newCategory.Id }, newCategory);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCategory request)
        {
            if (id == Guid.Empty) return BadRequest("Id invalido");
            await _useCase.Update(id, request);
            return NoContent();
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (id == Guid.Empty) return BadRequest("Id invalido");
            await _useCase.Delete(id);
            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _useCase.GetAll();
            return Ok(categories);
        }
    }
}
