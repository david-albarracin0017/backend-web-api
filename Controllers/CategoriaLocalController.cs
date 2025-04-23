using MicroService_NaceTuIdea.Interface;
using MicroService_NaceTuIdea.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroService_NaceTuIdea.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaLocalController : ControllerBase
    {
        private readonly ICategoriaLocal _categoriaLocalRepository;
        private readonly ILogger<CategoriaLocalController> _logger;

        public CategoriaLocalController(ICategoriaLocal categoriaLocalRepository, ILogger<CategoriaLocalController> logger)
        {
            _categoriaLocalRepository = categoriaLocalRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaLocal>>> GetAllCategoriasLocales()
        {
            try
            {
                _logger.LogInformation("Obteniendo todas las categorías locales.");
                var categoriasLocales = await _categoriaLocalRepository.GetAll();
                return Ok(categoriasLocales);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las categorías locales.");
                return StatusCode(500, "Ocurrió un error al procesar su solicitud.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaLocal>> GetCategoriaLocalById(Guid id)
        {
            try
            {
                _logger.LogInformation($"Obteniendo la categoría local con ID: {id}");
                var categoriaLocal = await _categoriaLocalRepository.GetById(id);

                if (categoriaLocal == null)
                {
                    _logger.LogWarning($"No se encontró la categoría local con ID: {id}");
                    return NotFound();
                }

                return Ok(categoriaLocal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener la categoría local con ID: {id}");
                return StatusCode(500, "Ocurrió un error al procesar su solicitud.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaLocal>> CreateCategoriaLocal(CategoriaLocal categoriaLocal)
        {
            try
            {
                _logger.LogInformation("Creando una nueva categoría local.");
                await _categoriaLocalRepository.Add(categoriaLocal);
                return CreatedAtAction(nameof(GetCategoriaLocalById), new { id = categoriaLocal.id }, categoriaLocal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear una nueva categoría local.");
                return StatusCode(500, "Ocurrió un error al procesar su solicitud.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoriaLocal(Guid id, CategoriaLocal categoriaLocal)
        {
            try
            {
                _logger.LogInformation($"Actualizando la categoría local con ID: {id}");
                if (id != categoriaLocal.id)
                {
                    _logger.LogError($"El ID proporcionado en la ruta ({id}) no coincide con el ID en el cuerpo de la solicitud ({categoriaLocal.id}).");
                    return BadRequest();
                }

                await _categoriaLocalRepository.Update(categoriaLocal);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar la categoría local con ID: {id}");
                return StatusCode(500, "Ocurrió un error al procesar su solicitud.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoriaLocal(Guid id)
        {
            try
            {
                _logger.LogInformation($"Eliminando la categoría local con ID: {id}");
                await _categoriaLocalRepository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar la categoría local con ID: {id}");
                return StatusCode(500, "Ocurrió un error al procesar su solicitud.");
            }
        }
    }
}
