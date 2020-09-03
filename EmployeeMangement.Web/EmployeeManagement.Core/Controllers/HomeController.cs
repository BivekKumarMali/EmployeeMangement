using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Web;
using EmployeeManagement.Web.Models;
using EmployeeManagement.Web.ViewModel;
using EmployeeMangement.Web.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeMangement.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IValidationRepository _validation;

        public HomeController(ILogger<HomeController> loggers, IValidationRepository validation)
        {
            _logger = loggers;
            _validation = validation;
        }

        public async Task<ActionResult> Index([Bind("Email,Password")] LogInViewModel? logInViewModel)
        {
                if (ModelState.IsValid && logInViewModel.Email != null)
                {
                    if (await _validation.CheckValidation(logInViewModel.Email, logInViewModel.Password))
                    {
                        return RedirectToAction("Index", "Employee");
                    }
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                }
                return View(logInViewModel);
            
        }
        [HttpPost]
        public IActionResult Logout()
        {
            _validation.Logout();
            return RedirectToAction("Index", "Home");
        } 
        public IActionResult Privacy()
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
