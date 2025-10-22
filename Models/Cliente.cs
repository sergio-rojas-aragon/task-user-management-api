using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTU.Api.Models
{
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClienteId { get; set; }
        [Required]
        public string Nombre { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        // foraneas

        public ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();


    }
}
