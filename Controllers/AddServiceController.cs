using MicroService_NaceTuIdea.Interface;
using MicroService_NaceTuIdea.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroService_NaceTuIdea.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AddServiceController : ControllerBase
    {
        private readonly IAddService _addServiceRepository;
        private readonly ILogger<AddServiceController> _logger;

        public AddServiceController(IAddService addServiceRepository, ILogger<AddServiceController> logger)
        {
            _addServiceRepository = addServiceRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddService>>> GetAllAddServices()
        {
            try
            {
                _logger.LogInformation("Obteniendo todos los servicios adicionales.");
                var addServices = await _addServiceRepository.GetAll();
                return Ok(addServices);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los servicios adicionales.");
                return StatusCode(500, "Ocurrió un error al procesar su solicitud.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AddService>> GetAddServiceById(Guid id)
        {
            try
            {
                _logger.LogInformation($"Obteniendo el servicio adicional con ID: {id}");
                var addService = await _addServiceRepository.GetById(id);

                if (addService == null)
                {
                    _logger.LogWarning($"No se encontró el servicio adicional con ID: {id}");
                    return NotFound();
                }

                return Ok(addService);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el servicio adicional con ID: {id}");
                return StatusCode(500, "Ocurrió un error al procesar su solicitud.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<AddService>> CreateAddService(AddService addService)
        {
            try
            {
                _logger.LogInformation("Creando un nuevo servicio adicional.");
                await _addServiceRepository.Add(addService);
                return CreatedAtAction(nameof(GetAddServiceById), new { id = addService.Id }, addService);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear un nuevo servicio adicional.");
                return StatusCode(500, "Ocurrió un error al procesar su solicitud.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddService(Guid id, AddService addService)
        {
            try
            {
                _logger.LogInformation($"Actualizando el servicio adicional con ID: {id}");
                if (id != addService.Id)
                {
                    _logger.LogError($"El ID proporcionado en la ruta ({id}) no coincide con el ID en el cuerpo de la solicitud ({addService.Id}).");
                    return BadRequest();
                }

                await _addServiceRepository.Update(addService);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar el servicio adicional con ID: {id}");
                return StatusCode(500, "Ocurrió un error al procesar su solicitud.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddService(Guid id)
        {
            try
            {
                _logger.LogInformation($"Eliminando el servicio adicional con ID: {id}");
                await _addServiceRepository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar el servicio adicional con ID: {id}");
                return StatusCode(500, "Ocurrió un error al procesar su solicitud.");
            }
        }
    }

}
