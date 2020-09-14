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
        private readonly ILogger<HomeController> _logger;
        private readonly IValidationRepository _validation;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly INotificationRepository _notificationRepository;

        public HomeController
            (
                ILogger<HomeController> loggers,
                IValidationRepository validation,
                UserManager<IdentityUser> userManager,
                IEmployeeRepository employeeRepository,
                INotificationRepository notificationRepository
            )
        {
            _logger = loggers;
            _validation = validation;
            _userManager = userManager;
            _employeeRepository = employeeRepository;
            _notificationRepository = notificationRepository;
        }

        public async Task<IActionResult> Index()
        {
            int Did = await CheckUserRole();
            if (Did == 0)
            {
                return RedirectToAction("Index","Employee");
            }
                return View(await _employeeRepository.FilterEmployee(Did));
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Profile(Employee employee)
        {
            if(employee.Eid != 0)
            {
                _employeeRepository.AddEmployee(employee);
                return RedirectToAction("Index");
            }
            else
            {
                var user = _userManager.GetUserAsync(HttpContext.User).Result;
                employee = await _employeeRepository.GetEmployeeByUserId(user.Id);
                return View(employee);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Notification()
        {
            NotificationViewModel notificationViewModel = new NotificationViewModel();
            int Did = await CheckUserRole();
            if (Did == 0)
            {
                notificationViewModel.Notifications = _notificationRepository.GetNotifications();
            }
            else
            {
                notificationViewModel.Notifications = _notificationRepository.GetNotificationsByDid(Did);
            }

            return Ok (new { UserNotification = notificationViewModel.Notifications , Count = notificationViewModel.Notifications.Count() });
        }

        public async Task<int> CheckUserRole()
        {
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            Employee employee = await _employeeRepository.GetEmployeeByUserId(user.Id);
            if (User.IsInRole("Admin") || User.IsInRole("HR"))
                return 0;
            else return employee.Did;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
