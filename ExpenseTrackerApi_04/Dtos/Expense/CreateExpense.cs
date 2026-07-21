using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerApi_04.Dtos.Expense
{
    public class CreateExpense
    {
        [Required(ErrorMessage = "La descripcion es obligatoria")]
        public string Description { get; set; }
        [Required(ErrorMessage = "El monto es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0.")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "La fecha de gasto es obligatoria")]
        public DateTimeOffset ExpenseDate { get; set; }
        [Required(ErrorMessage = "La categoria es obligatoria")]
        public Guid CategoryId { get; set; }
    }
}
