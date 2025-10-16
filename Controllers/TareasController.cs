using GTU.Api.Data;
using GTU.Api.DTOs;
using GTU.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace GTU.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : Controller
    {
        private AppDbContext _context;

        public TareasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> get()
        {
            var listado = await _context.Tareas.ToListAsync();
            return Ok(listado);
        }

        [HttpPost]
        public async Task<ActionResult> crearTarea([FromBody] TareaDTO tarea) {

            var task = new Tarea
            {
                Descripcion = tarea.Descripcion,
                Titulo = tarea.Titulo,
                Estado = tarea.Estado,
                UsuarioId = 1,

            };

            _context.Tareas.Add(task);
            await _context.SaveChangesAsync();
            return Ok(task);
        }
    }
}
