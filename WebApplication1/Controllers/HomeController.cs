using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(
            ILogger<HomeController> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            string environmentMessage = GetEnvironmentMessage();

            ViewBag.EnvironmentMessage = environmentMessage;

            _logger.LogInformation(
                "Numberwang home page loaded. Environment message: {EnvironmentMessage}",
                environmentMessage);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Play(int userNumber)
        {
            ViewBag.UserNumber = userNumber;

            string environmentMessage = GetEnvironmentMessage();
            ViewBag.EnvironmentMessage = environmentMessage;

            string result;

            // Condition 1: Correct number = guaranteed win
            if (userNumber == 12)
            {
                result = "That's Numberwang!";
            }
            else
            {
                // Otherwise, 1 in 4 chance of a random win
                Random random = new Random();
                int randomResult = random.Next(1, 5);

                if (randomResult == 1)
                {
                    result = "That's Numberwang!";
                }
                else
                {
                    result = "That's not Numberwang!";
                }
            }

            ViewBag.Result = result;

            _logger.LogInformation(
                "Numberwang played with number {UserNumber}. Result: {Result}",
                userNumber,
                result);

            return View("Index");
        }

        private string GetEnvironmentMessage()
        {
            return _configuration["NumberwangSettings:EnvironmentMessage"]
                ?? "Configuration value not found";
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}