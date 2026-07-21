using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerApi_04.Dtos.Category
{
    public class CreateCategory
    {
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La descripcion es obligatoria")]
        public string Description { get; set; }
    }
}
