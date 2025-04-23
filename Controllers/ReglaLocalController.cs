using MicroService_NaceTuIdea.Interface;
using MicroService_NaceTuIdea.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroService_NaceTuIdea.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Aplica autenticación a todo el controlador
    public class ReglaLocalController : ControllerBase
    {
        private readonly IReglaLocal _reglaLocalRepository;
        private readonly ILogger<ReglaLocalController> _logger;

        public ReglaLocalController(IReglaLocal reglaLocalRepository, ILogger<ReglaLocalController> logger)
        {
            _reglaLocalRepository = reglaLocalRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReglaLocal>>> GetAllReglasLocales()
        {
            try
            {
                var reglasLocales = await _reglaLocalRepository.GetAll();
                return Ok(reglasLocales);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las reglas locales");
                return StatusCode(500, "Ocurrió un error al obtener las reglas locales: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReglaLocal>> GetReglaLocalById(Guid id)
        {
            try
            {
                var reglaLocal = await _reglaLocalRepository.GetById(id);
                if (reglaLocal == null)
                {
                    return NotFound();
                }
                return Ok(reglaLocal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la regla local con ID {Id}", id);
                return StatusCode(500, $"Ocurrió un error al obtener la regla local con ID {id}: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ReglaLocal>> CreateReglaLocal(ReglaLocal reglaLocal)
        {
            try
            {
                await _reglaLocalRepository.Add(reglaLocal);
                return CreatedAtAction(nameof(GetReglaLocalById), new { id = reglaLocal.id }, reglaLocal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear una nueva regla local");
                return StatusCode(500, "Ocurrió un error al crear una nueva regla local: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReglaLocal(Guid id, ReglaLocal reglaLocal)
        {
            try
            {
                if (id != reglaLocal.id)
                {
                    return BadRequest();
                }
                await _reglaLocalRepository.Update(reglaLocal);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la regla local con ID {Id}", id);
                return StatusCode(500, $"Ocurrió un error al actualizar la regla local con ID {id}: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReglaLocal(Guid id)
        {
            try
            {
                await _reglaLocalRepository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la regla local con ID {Id}", id);
                return StatusCode(500, $"Ocurrió un error al eliminar la regla local con ID {id}: " + ex.Message);
            }
        }
    }
}
