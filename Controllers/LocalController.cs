using MicroService_NaceTuIdea.Interface;
using MicroService_NaceTuIdea.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroService_NaceTuIdea.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocalController : ControllerBase
    {
        private readonly ILocal _localRepository;
        private readonly ILogger<LocalController> _logger;

        public LocalController(ILocal localRepository, ILogger<LocalController> logger)
        {
            _localRepository = localRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Local>>> GetAllLocales()
        {
            try
            {
                var locales = await _localRepository.GetAll();
                return Ok(locales);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los locales");
                return StatusCode(500, "Ocurrió un error al obtener los locales: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Local>> GetLocalById(Guid id)
        {
            try
            {
                var local = await _localRepository.GetById(id);
                if (local == null)
                {
                    return NotFound();
                }
                return Ok(local);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el local con ID {Id}", id);
                return StatusCode(500, $"Ocurrió un error al obtener el local con ID {id}: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Local>> CreateLocal(Local local)
        {
            try
            {
                await _localRepository.Add(local);
                return CreatedAtAction(nameof(GetLocalById), new { id = local.id }, local);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear un nuevo local");
                return StatusCode(500, "Ocurrió un error al crear un nuevo local: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocal(Guid id, Local local)
        {
            try
            {
                if (id != local.id)
                {
                    return BadRequest();
                }
                await _localRepository.Update(local);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el local con ID {Id}", id);
                return StatusCode(500, $"Ocurrió un error al actualizar el local con ID {id}: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocal(Guid id)
        {
            try
            {
                await _localRepository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el local con ID {Id}", id);
                return StatusCode(500, $"Ocurrió un error al eliminar el local con ID {id}: " + ex.Message);
            }
        }
    }
}
