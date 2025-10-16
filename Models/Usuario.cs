using System.ComponentModel.DataAnnotations;

namespace GTU.Api.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }

        // Relacion uno a muchos
        public List<Tarea> Tareas { get; set; }

    }
}
