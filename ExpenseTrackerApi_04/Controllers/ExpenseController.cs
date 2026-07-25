using ExpenseTrackerApi_04.Application;
using ExpenseTrackerApi_04.Dtos.Expense;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerApi_04.Controllers
{
    [ApiController]
    [Route("api/expenses")]
    public class ExpenseController: ControllerBase
    {
        private readonly ExpenseUseCase _useCase;
        public ExpenseController(ExpenseUseCase useCase)
        {
            _useCase = useCase;
        }
        [HttpGet("{id:guid}")]// usar guid en la ruta hace que asp.net valide el formato correcto del dato
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            if (id == Guid.Empty) return BadRequest("El id es inválido.");
            var expense = await _useCase.GetById(id);
            return Ok(expense);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateExpense request)
        {
            var newExpense = await _useCase.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = newExpense.Id }, newExpense);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateExpense request)
        {
            if (id == Guid.Empty) return BadRequest("El id es inválido.");
            await _useCase.Update(id, request);
            return NoContent();
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (id == Guid.Empty) return BadRequest("El id es inválido.");
            await _useCase.Delete(id);
            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ExpenseQueryParameters query)
        {
            var expenses = await _useCase.GetAll(query);
            return Ok(expenses);
        }
        [HttpGet("by-category")]
        public async Task<IActionResult> GetByCategory()
        {
            var result = await _useCase.GetExpensesByCategory();
            return Ok(result);
        }
        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            var result = await _useCase.GetSummary();
            return Ok(result);
        }
    }
}
