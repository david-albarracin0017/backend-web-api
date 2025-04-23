using MicroService_NaceTuIdea.Interface;
using MicroService_NaceTuIdea.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroService_NaceTuIdea.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RespuestaRController : ControllerBase
    {
        private readonly IRespuestaR _respuestaRRepository;
        private readonly ILogger<RespuestaRController> _logger;

        public RespuestaRController(IRespuestaR respuestaRRepository, ILogger<RespuestaRController> logger)
        {
            _respuestaRRepository = respuestaRRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RespuestaR>>> GetAllRespuestas()
        {
            try
            {
                var respuestas = await _respuestaRRepository.GetAll();
                return Ok(respuestas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las respuestas");
                return StatusCode(500, "Ocurrió un error al obtener las respuestas: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RespuestaR>> GetRespuestaById(Guid id)
        {
            try
            {
                var respuesta = await _respuestaRRepository.GetById(id);
                if (respuesta == null)
                {
                    return NotFound();
                }
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la respuesta con ID {Id}", id);
                return StatusCode(500, $"Ocurrió un error al obtener la respuesta con ID {id}: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<RespuestaR>> CreateRespuesta(RespuestaR respuesta)
        {
            try
            {
                await _respuestaRRepository.Add(respuesta);
                return CreatedAtAction(nameof(GetRespuestaById), new { id = respuesta.id }, respuesta);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear una nueva respuesta");
                return StatusCode(500, "Ocurrió un error al crear una nueva respuesta: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRespuesta(Guid id, RespuestaR respuesta)
        {
            try
            {
                if (id != respuesta.id)
                {
                    return BadRequest();
                }
                await _respuestaRRepository.Update(respuesta);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la respuesta con ID {Id}", id);
                return StatusCode(500, $"Ocurrió un error al actualizar la respuesta con ID {id}: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRespuesta(Guid id)
        {
            try
            {
                await _respuestaRRepository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la respuesta con ID {Id}", id);
                return StatusCode(500, $"Ocurrió un error al eliminar la respuesta con ID {id}: " + ex.Message);
            }
        }
    }
}
