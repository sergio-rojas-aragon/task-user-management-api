using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTU.Api.Models
{
    public class Rol
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RolId { get; set; }
        [Required]
        public string Nombre { get; set; }

        // Relacion uno a muchos
        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();

    }
}
