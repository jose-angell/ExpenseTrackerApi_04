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
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            if (id == Guid.Empty) return BadRequest("El id es invalido");
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
            if (id == Guid.Empty) return BadRequest("El id es invalido");
            await _useCase.Update(id, request);
            return NoContent();
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (id == Guid.Empty) return BadRequest("El id es invalido");
            await _useCase.Delete(id);
            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ExpenseQueryParameters query)
        {
            var expenses = await _useCase.GetAll(query);
            return Ok(expenses);
        }
    }
}
