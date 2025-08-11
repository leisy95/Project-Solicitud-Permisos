using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PermisosApi.Data;

namespace PermisosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosUsuarios()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return Ok(usuarios);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
