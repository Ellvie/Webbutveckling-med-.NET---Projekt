using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Webbutveckling_med_.NET___Projekt.Data;
using Webbutveckling_med_.NET___Projekt.Models;

namespace Webbutveckling_med_.NET___Projekt.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DogContext _context;

        public HomeController(ILogger<HomeController> logger, DogContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Dog.Include(x => x.Person).OrderBy(x=>x.Added).Where(x => x.Reserved == false && x.Adopted == false).Take(3).ToList());
        }

        public IActionResult OurDogs()
        {
            return View(_context.Dog.Include(x => x.Person).Where(x => x.Reserved == false && x.Adopted == false).ToList());
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}