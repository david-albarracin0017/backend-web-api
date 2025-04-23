using MicroService_NaceTuIdea.Interface;
using MicroService_NaceTuIdea.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroService_NaceTuIdea.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotificacionController : ControllerBase
    {
        private readonly INotificacion _notificacionRepository;
        private readonly ILogger<NotificacionController> _logger;

        public NotificacionController(INotificacion notificacionRepository, ILogger<NotificacionController> logger)
        {
            _notificacionRepository = notificacionRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notificacion>>> GetAllNotificaciones()
        {
            try
            {
                var notificaciones = await _notificacionRepository.GetAll();
                return Ok(notificaciones);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las notificaciones");
                return StatusCode(500, "Ocurrió un error al obtener las notificaciones: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Notificacion>> GetNotificacionById(Guid id)
        {
            try
            {
                var notificacion = await _notificacionRepository.GetById(id);
                if (notificacion == null)
                {
                    return NotFound();
                }
                return Ok(notificacion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la notificación con ID {Id}", id);
                return StatusCode(500, $"Ocurrió un error al obtener la notificación con ID {id}: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Notificacion>> CreateNotificacion(Notificacion notificacion)
        {
            try
            {
                await _notificacionRepository.Add(notificacion);
                return CreatedAtAction(nameof(GetNotificacionById), new { id = notificacion.id }, notificacion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear una nueva notificación");
                return StatusCode(500, "Ocurrió un error al crear una nueva notificación: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotificacion(Guid id, Notificacion notificacion)
        {
            try
            {
                if (id != notificacion.id)
                {
                    return BadRequest();
                }
                await _notificacionRepository.Update(notificacion);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la notificación con ID {Id}", id);
                return StatusCode(500, $"Ocurrió un error al actualizar la notificación con ID {id}: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotificacion(Guid id)
        {
            try
            {
                await _notificacionRepository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la notificación con ID {Id}", id);
                return StatusCode(500, $"Ocurrió un error al eliminar la notificación con ID {id}: " + ex.Message);
            }
        }
    }
}
