using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTU.Api.Models
{
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductoId { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Categoria { get; set; }

        [Required]
        public decimal Precio { get; set; }
        [Required]
        public DateTime fechaCreacion { get; set; }
        [Required]
        public DateTime fechaActualizacion { get; set; } 

        public ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    }
}
