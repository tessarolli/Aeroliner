using Aeroliner.Models;
using Aeroliner.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aeroliner.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightsController : Controller
    {
        private readonly FlightsService _flightsService;

        public FlightsController(FlightsService flightsService) =>
            _flightsService = flightsService;


        [HttpGet]
        public async Task<List<Flight>> Get() =>
            await _flightsService.GetAsync();


        [HttpGet("{id}")]
        public async Task<ActionResult<Flight>> Get(string id)
        {
            if (Guid.TryParse(id, out var flightId))
            {
                var flight = await _flightsService.GetAsync(flightId);

                if (flight is null)
                {
                    return NotFound();
                }

                return flight;
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Create(Flight newFlight)
        {
            newFlight.Id = new Guid();
            await _flightsService.CreateAsync(newFlight);

            return CreatedAtAction(nameof(Create), new { id = newFlight.Id }, newFlight);
        }


        [HttpPost("{id}")]
        public async Task<IActionResult> Update(string id, Flight updatedFlight)
        {
            if (Guid.TryParse(id, out var flightId))
            {
                var flight = await _flightsService.GetAsync(flightId);

                if (flight is null)
                {
                    return NotFound();
                }

                updatedFlight.Id = flight.Id;

                await _flightsService.UpdateAsync(flightId, updatedFlight);

                return NoContent();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (Guid.TryParse(id, out var flightId))
            {
                var flight = await _flightsService.GetAsync(flightId);

                if (flight is null)
                {
                    return NotFound();
                }

                await _flightsService.RemoveAsync(flightId);

                return NoContent();
            }

            return BadRequest();
        }
    }
}
