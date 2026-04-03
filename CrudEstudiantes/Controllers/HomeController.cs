using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CrudEstudiantes.Models;

namespace CrudEstudiantes.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}