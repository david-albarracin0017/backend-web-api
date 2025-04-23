using MicroService_NaceTuIdea.Interface;
using MicroService_NaceTuIdea.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroService_NaceTuIdea.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Aplica autenticación a todo el controlador
    public class ReservaController : ControllerBase
    {
        private readonly IReserva _reservaRepository;
        private readonly ILogger<ReservaController> _logger;

        public ReservaController(IReserva reservaRepository, ILogger<ReservaController> logger)
        {
            _reservaRepository = reservaRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reserva>>> GetAllReservas()
        {
            try
            {
                var reservas = await _reservaRepository.GetAll();
                return Ok(reservas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las reservas");
                return StatusCode(500, "Ocurrió un error al obtener las reservas: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reserva>> GetReservaById(Guid id)
        {
            try
            {
                var reserva = await _reservaRepository.GetById(id);
                if (reserva == null)
                {
                    return NotFound();
                }
                return Ok(reserva);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la reserva con ID {Id}", id);
                return StatusCode(500, $"Ocurrió un error al obtener la reserva con ID {id}: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Reserva>> CreateReserva(Reserva reserva)
        {
            try
            {
                await _reservaRepository.Add(reserva);
                return CreatedAtAction(nameof(GetReservaById), new { id = reserva.id }, reserva);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear una nueva reserva");
                return StatusCode(500, "Ocurrió un error al crear una nueva reserva: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReserva(Guid id, Reserva reserva)
        {
            try
            {
                if (id != reserva.id)
                {
                    return BadRequest();
                }
                await _reservaRepository.Update(reserva);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la reserva con ID {Id}", id);
                return StatusCode(500, $"Ocurrió un error al actualizar la reserva con ID {id}: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReserva(Guid id)
        {
            try
            {
                await _reservaRepository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la reserva con ID {Id}", id);
                return StatusCode(500, $"Ocurrió un error al eliminar la reserva con ID {id}: " + ex.Message);
            }
        }
    }
}
