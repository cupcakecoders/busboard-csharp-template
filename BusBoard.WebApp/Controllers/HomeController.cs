using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BusBoard.WebApp.Models;
using BusBoard.WebApp.ViewModel;

namespace BusBoard.WebApp.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("bus-info")]
        public IActionResult BusInfo(PostcodeSelection selection)
        {
            var info = new BusInfo(selection.Postcode);
            return View(info);
        }

        [HttpGet("about")]
        public IActionResult About()
        {
            ViewData["Message"] = "Information about this site";
            return View();
        }

        [HttpGet("contact-us")]
        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet("error")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}