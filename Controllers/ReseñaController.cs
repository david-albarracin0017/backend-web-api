using MicroService_NaceTuIdea.Interface;
using MicroService_NaceTuIdea.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroService_NaceTuIdea.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReseñaController : ControllerBase
    {
        private readonly IReseña _reseñaRepository;
        private readonly ILogger<ReseñaController> _logger;

        public ReseñaController(IReseña reseñaRepository, ILogger<ReseñaController> logger)
        {
            _reseñaRepository = reseñaRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reseña>>> GetAllReseñas()
        {
            try
            {
                var reseñas = await _reseñaRepository.GetAll();
                return Ok(reseñas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las reseñas");
                return StatusCode(500, "Ocurrió un error al obtener las reseñas: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reseña>> GetReseñaById(Guid id)
        {
            try
            {
                var reseña = await _reseñaRepository.GetById(id);
                if (reseña == null)
                {
                    return NotFound();
                }
                return Ok(reseña);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la reseña con ID {Id}", id);
                return StatusCode(500, $"Ocurrió un error al obtener la reseña con ID {id}: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Reseña>> CreateReseña(Reseña reseña)
        {
            try
            {
                await _reseñaRepository.Add(reseña);
                return CreatedAtAction(nameof(GetReseñaById), new { id = reseña.id }, reseña);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear una nueva reseña");
                return StatusCode(500, "Ocurrió un error al crear una nueva reseña: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReseña(Guid id, Reseña reseña)
        {
            try
            {
                if (id != reseña.id)
                {
                    return BadRequest();
                }
                await _reseñaRepository.Update(reseña);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la reseña con ID {Id}", id);
                return StatusCode(500, $"Ocurrió un error al actualizar la reseña con ID {id}: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReseña(Guid id)
        {
            try
            {
                await _reseñaRepository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la reseña con ID {Id}", id);
                return StatusCode(500, $"Ocurrió un error al eliminar la reseña con ID {id}: " + ex.Message);
            }
        }
    }
}
