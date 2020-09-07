using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Web.Models;
using EmployeeManagement.Web.ViewModel;
using EmployeeMangement.Web.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMangement.Web.EmployeeManagement.Core.Controllers
{
    public class AccountController : Controller
    {
        
        private readonly IValidationRepository _validation;
        private readonly IManager _manager;
        private readonly IEmployeeRepository _employeeRepository;

        public AccountController(IValidationRepository validation, IManager manager, IEmployeeRepository employeeRepository)
        {
            _validation = validation;
            _manager = manager;
            _employeeRepository = employeeRepository;
        }

        public async Task<ActionResult> Login([Bind("Email,Password")] LogInViewModel logInViewModel)
        {
            if (ModelState.IsValid && logInViewModel.Email != null)
            {
                if (await _validation.CheckValidation(logInViewModel.Email, logInViewModel.Password))
                {
                    var User = await _manager.GetUserByEmail(logInViewModel.Email);
                    string UserId = User.Id;
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(logInViewModel);

        }

        public IActionResult Logout()
        {
            _validation.Logout();
            return RedirectToAction("Login", "Account");
        }
        public async Task<IActionResult> ForgetPassword(PasswordResetView passwordResetView)
        {
            if(passwordResetView.Email != null) 
            {
                if( await _manager.UpdatePassword(passwordResetView))
                {
                    return RedirectToAction("Login");
                }
            }
            ModelState.AddModelError(string.Empty, "Wrong Email");
            return View();
        }

    }
}
