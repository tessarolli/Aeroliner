using Aeroliner.Helpers;
using Aeroliner.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aeroliner.Areas.Razor.Controllers
{
    [Area("Razor")]
    public class HomeController : Controller
    {
        readonly HttpClientHelper _httpClient;

        public HomeController(HttpClient httpClient)
        {
            _httpClient = new HttpClientHelper(httpClient);
        }

        public async Task<IActionResult> IndexAsync()
        {
            IEnumerable<Flight>? flights = null;

            // Get Flight List from API, from the Backend Server
            // This API access method should be used when the API server is not the same as the MVC server, 
            // and data from the API are required by the business rule of this Controller Action
            flights = await _httpClient.GetObjectAsync<IEnumerable<Flight>>("https://localhost:5000/api/flights");

            return View(flights);
        }
    }
}
