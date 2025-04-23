using MicroService_NaceTuIdea.Interface;
using MicroService_NaceTuIdea.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroService_NaceTuIdea.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComodidadLocalController : ControllerBase
    {
        private readonly IComodidadLocal _comodidadLocalRepository;
        private readonly ILogger<ComodidadLocalController> _logger;

        public ComodidadLocalController(IComodidadLocal comodidadLocalRepository, ILogger<ComodidadLocalController> logger)
        {
            _comodidadLocalRepository = comodidadLocalRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComodidadLocal>>> GetAllComodidadesLocales()
        {
            try
            {
                _logger.LogInformation("Obteniendo todas las comodidades locales.");
                var comodidadesLocales = await _comodidadLocalRepository.GetAll();
                return Ok(comodidadesLocales);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las comodidades locales.");
                return StatusCode(500, "Ocurrió un error al procesar su solicitud.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ComodidadLocal>> GetComodidadLocalById(Guid id)
        {
            try
            {
                _logger.LogInformation($"Obteniendo la comodidad local con ID: {id}");
                var comodidadLocal = await _comodidadLocalRepository.GetById(id);

                if (comodidadLocal == null)
                {
                    _logger.LogWarning($"No se encontró la comodidad local con ID: {id}");
                    return NotFound();
                }

                return Ok(comodidadLocal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener la comodidad local con ID: {id}");
                return StatusCode(500, "Ocurrió un error al procesar su solicitud.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ComodidadLocal>> CreateComodidadLocal(ComodidadLocal comodidadLocal)
        {
            try
            {
                _logger.LogInformation("Creando una nueva comodidad local.");
                await _comodidadLocalRepository.Add(comodidadLocal);
                return CreatedAtAction(nameof(GetComodidadLocalById), new { id = comodidadLocal.id }, comodidadLocal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear una nueva comodidad local.");
                return StatusCode(500, "Ocurrió un error al procesar su solicitud.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComodidadLocal(Guid id, ComodidadLocal comodidadLocal)
        {
            try
            {
                _logger.LogInformation($"Actualizando la comodidad local con ID: {id}");
                if (id != comodidadLocal.id)
                {
                    _logger.LogError($"El ID proporcionado en la ruta ({id}) no coincide con el ID en el cuerpo de la solicitud ({comodidadLocal.id}).");
                    return BadRequest();
                }

                await _comodidadLocalRepository.Update(comodidadLocal);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar la comodidad local con ID: {id}");
                return StatusCode(500, "Ocurrió un error al procesar su solicitud.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComodidadLocal(Guid id)
        {
            try
            {
                _logger.LogInformation($"Eliminando la comodidad local con ID: {id}");
                await _comodidadLocalRepository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar la comodidad local con ID: {id}");
                return StatusCode(500, "Ocurrió un error al procesar su solicitud.");
            }
        }
    }
}
