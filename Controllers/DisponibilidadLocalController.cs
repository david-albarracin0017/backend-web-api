using MicroService_NaceTuIdea.Interface;
using MicroService_NaceTuIdea.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroService_NaceTuIdea.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DisponibilidadLocalController : ControllerBase
    {
        private readonly IDisponibilidadLocal _disponibilidadLocalRepository;
        private readonly ILogger<DisponibilidadLocalController> _logger;

        public DisponibilidadLocalController(IDisponibilidadLocal disponibilidadLocalRepository, ILogger<DisponibilidadLocalController> logger)
        {
            _disponibilidadLocalRepository = disponibilidadLocalRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DisponibilidadLocal>>> GetAllDisponibilidadesLocales()
        {
            try
            {
                _logger.LogInformation("Obteniendo todas las disponibilidades locales.");
                var disponibilidadesLocales = await _disponibilidadLocalRepository.GetAll();
                return Ok(disponibilidadesLocales);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las disponibilidades locales.");
                return StatusCode(500, "Ocurrió un error al procesar su solicitud.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DisponibilidadLocal>> GetDisponibilidadLocalById(Guid id)
        {
            try
            {
                _logger.LogInformation($"Obteniendo la disponibilidad local con ID: {id}");
                var disponibilidadLocal = await _disponibilidadLocalRepository.GetById(id);

                if (disponibilidadLocal == null)
                {
                    _logger.LogWarning($"No se encontró la disponibilidad local con ID: {id}");
                    return NotFound();
                }

                return Ok(disponibilidadLocal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener la disponibilidad local con ID: {id}");
                return StatusCode(500, "Ocurrió un error al procesar su solicitud.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<DisponibilidadLocal>> CreateDisponibilidadLocal(DisponibilidadLocal disponibilidadLocal)
        {
            try
            {
                _logger.LogInformation("Creando una nueva disponibilidad local.");
                await _disponibilidadLocalRepository.Add(disponibilidadLocal);
                return CreatedAtAction(nameof(GetDisponibilidadLocalById), new { id = disponibilidadLocal.id }, disponibilidadLocal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear una nueva disponibilidad local.");
                return StatusCode(500, "Ocurrió un error al procesar su solicitud.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDisponibilidadLocal(Guid id, DisponibilidadLocal disponibilidadLocal)
        {
            try
            {
                _logger.LogInformation($"Actualizando la disponibilidad local con ID: {id}");
                if (id != disponibilidadLocal.id)
                {
                    _logger.LogError($"El ID proporcionado en la ruta ({id}) no coincide con el ID en el cuerpo de la solicitud ({disponibilidadLocal.id}).");
                    return BadRequest();
                }

                await _disponibilidadLocalRepository.Update(disponibilidadLocal);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar la disponibilidad local con ID: {id}");
                return StatusCode(500, "Ocurrió un error al procesar su solicitud.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDisponibilidadLocal(Guid id)
        {
            try
            {
                _logger.LogInformation($"Eliminando la disponibilidad local con ID: {id}");
                await _disponibilidadLocalRepository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar la disponibilidad local con ID: {id}");
                return StatusCode(500, "Ocurrió un error al procesar su solicitud.");
            }
        }
    }
}
