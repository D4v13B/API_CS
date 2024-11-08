using API_LEARN.Data;
using API_LEARN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_LEARN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        //Crear un nuevo usuario
        [HttpPost]
        public async Task<ActionResult<User>> SaveUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Devuelve los errores de validacion
            }

            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetUser), new { id = user.id }, user);
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Ocurrio un error al crear");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            try
            {
                var user = _context.Users.Find(id);

                if (user == null)
                {
                    return NotFound();
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return Ok(user);
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Error al eliminar al usuario");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, User updateUser)
        {
            if (id != updateUser.id)
            {
                return BadRequest("El ID del usuario en la URL no coincide del Objeto");
            }

            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                user.name = updateUser.name;
                user.email = updateUser.email;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return Ok(user);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Error al actualizar");
            }
        }
    }
}
