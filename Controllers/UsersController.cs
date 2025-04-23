using MicroService_NaceTuIdea.Interface;
using MicroService_NaceTuIdea.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroService_NaceTuIdea.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUsers _usersRepository;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUsers usersRepository, ILogger<UsersController> logger)
        {
            _usersRepository = usersRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetAllUsers()
        {
            try
            {
                var users = await _usersRepository.GetAll();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los usuarios");
                return StatusCode(500, "Ocurrió un error al obtener los usuarios: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUserById(Guid id)
        {
            try
            {
                var user = await _usersRepository.GetById(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el usuario con ID {Id}", id);
                return StatusCode(500, $"Ocurrió un error al obtener el usuario con ID {id}: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Users>> CreateUser(Users user)
        {
            try
            {
                await _usersRepository.Add(user);
                return CreatedAtAction(nameof(GetUserById), new { id = user.id }, user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear un nuevo usuario");
                return StatusCode(500, "Ocurrió un error al crear un nuevo usuario: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, Users user)
        {
            try
            {
                if (id != user.id)
                {
                    return BadRequest();
                }
                await _usersRepository.Update(user);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el usuario con ID {Id}", id);
                return StatusCode(500, $"Ocurrió un error al actualizar el usuario con ID {id}: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                await _usersRepository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el usuario con ID {Id}", id);
                return StatusCode(500, $"Ocurrió un error al eliminar el usuario con ID {id}: " + ex.Message);
            }
        }
    }
}
