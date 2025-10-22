using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTU.Api.Models
{
    public class Tarea
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TareaId { get; set; }

        [Required]
        public string Titulo { get; set; }
        public string Descripcion { get; set; }

        [Required]
        public string Estado { get; set; }

        // claves foraneas

        // usuario
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        //estado
        public int EstadoId { get; set; }
        public EstadoTarea estadoTarea { get; set; } = null!;

        //cliente
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;

        // pedido
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; } = null!;

    }
}
