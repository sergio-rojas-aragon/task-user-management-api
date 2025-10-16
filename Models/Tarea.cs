﻿namespace GTU.Api.Models
{
    public class Tarea
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }

        // claves foraneas

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
