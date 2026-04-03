using System.ComponentModel.DataAnnotations;

namespace CrudEstudiantes.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(50, ErrorMessage = "El apellido no puede tener más de 50 caracteres")]
        public string LastName { get; set; } = string.Empty;

        [Range(1, 120, ErrorMessage = "La edad debe estar entre 1 y 120")]
        public int Age { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "El email no es válido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La carrera es obligatoria")]
        [StringLength(100, ErrorMessage = "La carrera no puede tener más de 100 caracteres")]
        public string Major { get; set; } = string.Empty;
    }
}