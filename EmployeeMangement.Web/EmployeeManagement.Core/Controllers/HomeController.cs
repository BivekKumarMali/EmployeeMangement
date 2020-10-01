using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Web;
using EmployeeManagement.Web.Models;
using EmployeeManagement.Web.ViewModel;
using EmployeeManagement.Web.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IManager _manager;

        public HomeController
            (
                ILogger<HomeController> loggers,
                IValidationRepository validation,
                IEmployeeRepository employeeRepository,
                INotificationRepository notificationRepository,
                IManager manager
            )
        {
            _employeeRepository = employeeRepository;
            _notificationRepository = notificationRepository;
            _manager = manager;
        }

        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin") || User.IsInRole("HR"))
            {
                return RedirectToAction("Index","Employee");
            }
            int Did = await GetDepartmentId(_manager.GetUserID(HttpContext.User));
            return View(await _employeeRepository.FilterEmployee(Did));
            
        }

        public async Task<int> GetDepartmentId(string userId)
        {
            Employee employee = _employeeRepository.GetEmployeeByUserId(userId);
            return employee.Did;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
