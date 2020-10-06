﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Web.Models;
using EmployeeManagement.Web.Repository;
using Microsoft.AspNetCore.Authorization;
using EmployeeManagement.Web.ViewModel;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Web.Http.Cors;

namespace EmployeeManagement.Web.EmployeeManagement.Core.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IManager _manager;
        private readonly EmployeeViewModel employeeViewModel;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly INotificationRepository _notificationRepository;

        public EmployeeController
            (
                AppDbContext context,
                IEmployeeRepository employeeRepository,
                IManager manager,
                UserManager<IdentityUser> userManager,
                INotificationRepository notificationRepository
            )
        {
            _context = context;
            _employeeRepository = employeeRepository;
            _manager = manager;
            employeeViewModel = new EmployeeViewModel();
            _userManager = userManager;
            _notificationRepository = notificationRepository;
        }

        public async Task<IEnumerable<Employee>> Index()
        {
            var list = await _employeeRepository.GetAllEmployees();
            return list;
        }
        [HttpGet]
        [Authorize(Roles = "Admin, HR")]
        public async Task<IEnumerable<Employee>> GetEmployees()
        {

            var list = await _employeeRepository.GetAllEmployees();
            return list;
        }

        [HttpGet("{Did}")]
        [Route("ByDid/{Did}")]
        public async Task<IEnumerable<Employee>> GetEmployeesByDepartmentID(int Did)
        {
            IEnumerable<Employee> Employees = await _employeeRepository.GetAllEmployees();
            return Employees.Where(e => e.Did == Did);
        }

        [HttpGet("{eid}")]
        [Route("ByEid/{eid}")]
        [Authorize(Roles = "Admin, HR")]
        public Employee GetEmployeeByEid(int eid) 
        {
            return _employeeRepository.GetEmployeeById(eid);
        }
        [HttpGet("{userId}")]
        [Route("ByUserID/{userId}")]
        public Employee GetEmployeeByEid(string userId) 
        {
            return _employeeRepository.GetEmployeeByUserId(userId);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, HR")]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            employee.UserId = await _manager.AddUserManager(employee);
            if (employee.UserId != null)
            {
                _employeeRepository.AddEmployee(employee);
                await _notificationRepository.AddEmployeeNotification(employee);
                return NoContent();
            }
            _notificationRepository.SendNotification("Deleted Employee", employee.Did);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(Employee employee)
        {
            if (await _manager.UpdateUserManager(employee))
            {
                _employeeRepository.UpdateEmployee(employee);
                await _notificationRepository.EditEmployeeNotification(employee);
            }
            _notificationRepository.SendNotification("Edit Employee", employee.Did);
            return NoContent();
        }

        
        [HttpDelete("{eid}")]
        [Authorize(Roles = "Admin, HR")]
        public async Task<IActionResult> DeleteEmployee(int eid)
        {
            Employee employee = _employeeRepository.GetEmployeeById(eid);
            int? Did = employee.Did;
            if (await _manager.DeleteUserManager(employee.UserId))
            {
                _employeeRepository.DeleteEmployee(eid);
                await _notificationRepository.DeleteEmployeeNotification(employee);
            }
            _notificationRepository.SendNotification("Deleted Employee", Did);
            return RedirectToAction(nameof(Index));
        }

    }
}
