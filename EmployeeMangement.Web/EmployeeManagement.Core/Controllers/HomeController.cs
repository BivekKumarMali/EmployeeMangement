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
            _logger = loggers;
            _validation = validation;
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

                string userID = _manager.GetUserID(HttpContext.User);
                employee = await _employeeRepository.GetEmployeeByUserId(userID);
                return View(employee);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Notification()
        {
            NotificationViewModel notificationViewModel = new NotificationViewModel();
            if (User.IsInRole("Admin") || User.IsInRole("HR"))
            {
                notificationViewModel.Notifications = _notificationRepository.GetNotifications(_manager.GetUserID(HttpContext.User));
            }
            else
            {
                int Did = await GetDepartmentId(_manager.GetUserID(HttpContext.User));
                notificationViewModel.Notifications = _notificationRepository.GetNotificationsByDid(Did, _manager.GetUserID(HttpContext.User));
            }

            return Ok(new { UserNotification = notificationViewModel.Notifications, Count = notificationViewModel.Notifications.Count() });
        }

        public void ReadNotification(int Nid)
        {
            string userId = _manager.GetUserID(HttpContext.User);
            _notificationRepository.IsReadNotification(Nid, userId);
        }

        public async Task<int> GetDepartmentId(string userId)
        {
            Employee employee = await _employeeRepository.GetEmployeeByUserId(userId);
            return employee.Did;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
