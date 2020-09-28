using System.Linq;
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
            return await _employeeRepository.GetAllEmployees();
        }
        [HttpGet]
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _employeeRepository.GetAllEmployees();
        }

        [HttpGet("{Did}")]
        public async Task<IEnumerable<Employee>> GetEmployeesByDepartmentID(int Did)
        {
            IEnumerable<Employee> Employees = await _employeeRepository.GetAllEmployees();
            return Employees.Where(e => e.Did == Did);
        }

        public IActionResult Create()
        {
            ViewData["DepartmentName"] = _employeeRepository.DepartmentListName();
            ViewData["RolesName"] = _employeeRepository.RoleListName();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Eid,Name,Surname,Address,Qualification,ContactNumber,Did,RoleId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.UserId = await _manager.AddUserManager(employee);
                if(employee.UserId != null)
                {
                    _employeeRepository.AddEmployee(employee);

                    string userID = _manager.GetUserID(HttpContext.User);
                    Employee currentEmployee = await _employeeRepository.GetEmployeeByUserId(userID);

                    await _notificationRepository.AddEmployeeNotification(employee);

                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["DepartmentName"] = _employeeRepository.DepartmentListId(employee.Did);
            ViewData["RolesName"] = _employeeRepository.RoleListName();
            return View(employee);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Employee employee = _employeeRepository.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["DepartmentName"] = _employeeRepository.DepartmentListName(employee.Did);
            ViewData["RolesName"] = _employeeRepository.RoleListName();
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Eid,Name,Surname,Address,Qualification,ContactNumber,Did,UserId,RoleId")] Employee employee)
        {
            if (id != employee.Eid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    if (await _manager.UpdateUserManager(employee))
                    {
                        _employeeRepository.UpdateEmployee(employee);

                        string userID = _manager.GetUserID(HttpContext.User);
                        Employee currentEmployee = await _employeeRepository.GetEmployeeByUserId(userID);

                        await _notificationRepository.EditEmployeeNotification(employee);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Eid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentName"] = _employeeRepository.DepartmentListId(employee.Did);
            ViewData["RolesName"] = _employeeRepository.RoleListName();
            return View(employee);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Employee employee = _employeeRepository.GetEmployeeById(id);
            if (await _manager.DeleteUserManager(employee.UserId))
            {
                _employeeRepository.DeleteEmployee(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Eid == id);
        }

    }
}
