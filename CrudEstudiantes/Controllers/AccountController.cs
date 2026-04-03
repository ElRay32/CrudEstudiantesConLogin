using CrudEstudiantes.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CrudEstudiantes.Controllers
{
    public class AccountController : Controller
    {
        private const string ValidUsername = "admin";
        private const string ValidPassword = "123456";

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                return RedirectToAction("Index", "Students");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Username == ValidUsername && model.Password == ValidPassword)
            {
                HttpContext.Session.SetString("Username", model.Username);
                return RedirectToAction("Index", "Students");
            }

            ViewBag.Error = "Usuario o contraseña incorrectos";
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}