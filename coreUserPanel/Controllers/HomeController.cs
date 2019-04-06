using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreUserPanel.Models;
using Microsoft.AspNetCore.Mvc;

namespace coreUserPanel.Controllers
{
    public class HomeController : Controller
    {
        ProjectTestDataContext context = new ProjectTestDataContext();
        public IActionResult Index()
        {
            var locations = context.Locations.ToList();
            return View(locations);
        }

        public IActionResult Multiplexes(int id)
        {
            var multiplexes = context.Multiplexes.Where(m => m.LocationId == id).ToList();
            return View(multiplexes);
        }

        public IActionResult Movies(int id)
        {
            var movies = context.Movies.Where(m => m.MultiplexId == id).ToList();
            return View(movies);
        }
       
    }
}