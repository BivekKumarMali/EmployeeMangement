using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using EmployeeManagement.Web.Models;
using EmployeeManagement.Web.Repository;
using EmployeeManagement.Web.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace EmployeeManagement.Web.EmployeeManagement.Core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class NotificationController : ControllerBase
    {
        private readonly IHubContext<SignalServer> _hub;
        private INotificationRepository _notificationRepository;
        private readonly IManager _manager;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public NotificationController(
            IHubContext<SignalServer> hub,
            INotificationRepository notificationRepository,
            IManager manager,
            IEmployeeRepository employeeRepository,
            UserManager<IdentityUser> userManager
            )
        {
            _hub = hub;
            _notificationRepository = notificationRepository;
            _manager = manager;
            _employeeRepository = employeeRepository;
            _userManager = userManager;
        }
        [HttpGet("{userID}")]
        public IActionResult Get(string userID)
        {
            var result = Notification(userID);
            var timerManager = new TimerManager(() => _hub.Clients.All.SendAsync("transferchartdata", result.Result));
            return Ok(new { result.Result});
        }
        [HttpPost]
        public async Task<IEnumerable<Notification>> Notification(string userID)
        {
            IdentityRole role = new IdentityRole();
            Employee employee = _employeeRepository.GetEmployeeByUserId(userID);
            Task<IdentityRole> t = _manager.GetRoleById(employee.RoleId);
            role = t.Result;
            if ( (role.Name == "Admin") || (role.Name == "HR"))
            {
                return _notificationRepository.GetNotifications(userID);
            }
            else
            {
                return _notificationRepository.GetNotificationsByDid(employee.Did,userID);
            }
        }
    }
}
