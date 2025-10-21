using GTU.Api.Data;
using GTU.Api.DTOs;
using GTU.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace GTU.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private AppDbContext _context;

        public TareasController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista todas las tareas
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> get()
        {
            var listado = await _context.Tareas.ToListAsync();
            return Ok(listado);
        }

        /// <summary>
        /// Lista todas las tareas
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult> getById(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea != null)
            {
                return Ok(tarea);
            }
            else
            {
                return NotFound();
            }
       
        }

        /// <summary>
        /// Agrega Tareas al usuario asignado
        /// </summary>
        /// <param name="tarea"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> crearTarea([FromBody] TareaDTO tarea) {

            var task = new Tarea
            {
                Descripcion = tarea.Descripcion,
                Titulo = tarea.Titulo,
                Estado = tarea.Estado,
                UsuarioId = tarea.UsuarioId,

            };

            _context.Tareas.Add(task);
            await _context.SaveChangesAsync();
            return Ok(task);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> actualizarTarea(int id,[FromBody] TareaDTO tarea)
        {

            var tareaFind = await _context.Tareas.FindAsync(id);
            if (tareaFind != null)
            {
                tareaFind.Descripcion = tarea.Descripcion;
                tareaFind.Estado = tarea.Estado;
                tareaFind.Titulo = tarea.Titulo;

                await _context.SaveChangesAsync();

                return Ok(tareaFind);

            }else { return NotFound(); }
                
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> eliminarTarea(int id)
        {
            var tareaFind = await _context.Tareas.FindAsync(id);
            if (tareaFind != null)
            {
                _context.Tareas.Remove(tareaFind);
                await _context.SaveChangesAsync();
                
                return Ok();

            } else { return NotFound(); }
            
        }
    }
}
