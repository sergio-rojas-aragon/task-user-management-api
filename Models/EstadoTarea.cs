using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTU.Api.Models
{
    public class EstadoTarea
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EstadoTareaId { get; set; }

        [Required]
        public string Nombre { get; set; }

        // relacion uno a muchos
        public ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
    }
}
