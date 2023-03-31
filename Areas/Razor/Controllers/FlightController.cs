using Aeroliner.Helpers;
using Aeroliner.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aeroliner.Areas.Razor.Controllers
{
    [Area("Razor")]
    [Route("/razor/flight")]
    public class FlightController : Controller
    {
        readonly HttpClientHelper _httpClient;

        public FlightController(HttpClient httpClient)
        {
            _httpClient = new HttpClientHelper(httpClient);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> EditAsync(string? id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var flight = await _httpClient.GetObjectAsync<Flight>($"https://localhost:5000/api/flights/{id}");
                return View("Flight", flight);
            }

            return View("Flight");
        }

        public IActionResult Add()
        {
            return View("Flight");
        }
    }
}
