using Microsoft.AspNetCore.Mvc;
using VehicleInventory.Application.DTOs;
using VehicleInventory.Application.Services;
using VehicleInventory.Domain.Exceptions;

namespace VehicleInventory.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/vehicles")]
    public class JPVehiclesController : ControllerBase
    {
        private readonly JPVehicleService _service;

        public JPVehiclesController(JPVehicleService service)
        {
            _service = service;
        }

        //GET: api/v1/vehicles
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vehicles = await _service.GetAllVehiclesAsync();
            return Ok(vehicles);
        }

        //GET: api/v1/vehicles/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var vehicle = await _service.GetVehicleByIdAsync(id);

            if (vehicle == null)
                return NotFound();

            return Ok(vehicle);
        }

        //POST: api/v1/vehicles
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] JPCreateVehicleDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _service.CreateVehicleAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        //PUT: api/v1/vehicles/{id}/status
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] JPUpdateVehicleStatusDto dto)
        {
            try
            {
                await _service.UpdateVehicleStatusAsync(id, dto);
                return NoContent();
            }
            catch (JPDomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //DELETE: api/v1/vehicles/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteVehicleAsync(id);
            return NoContent();
        }
    }
}
