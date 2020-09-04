using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Web.Models;
using EmployeeMangement.Web.Models;
using EmployeeMangement.Web.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeMangement.Web.EmployeeManagement.Core.Controllers
{

    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IManager _manager;

        public EmployeeController(AppDbContext context, IEmployeeRepository employeeRepository, IManager manager)
        {
            _context = context;
            _employeeRepository = employeeRepository;
            _manager = manager;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _employeeRepository.GetAllEmployees());
        }

        public IActionResult Create()
        {
            ViewData["DepartmentName"] = _employeeRepository.DepartmentListName();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Eid,Name,Surname,Address,Qualification,Email,Password,ContactNumber,Did")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (await _manager.AddUserManager(employee.Email, employee.Did))
                {
                    _employeeRepository.AddEmployee(employee);
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["DepartmentName"] = _employeeRepository.DepartmentListId(employee.Did);
            return View(employee);
        }

        public async Task<IActionResult> Edit(int? id)
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
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Eid,Name,Surname,Address,Qualification,ContactNumber,Did")] Employee employee)
        {
            if (id != employee.Eid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    if (await _manager.UpdateUserManager(employee.Email))
                    {
                        _employeeRepository.UpdateEmployee(employee);
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
            ViewData["Did"] = _employeeRepository.DepartmentListId(employee.Did);
            return View(employee);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Employee employee = _employeeRepository.GetEmployeeById(id);
            _employeeRepository.DeleteEmployee(id);
           // var newUser = await _userManager.FindByEmailAsync(employee.Email);
           // var result = await _userManager.DeleteAsync(newUser);
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Eid == id);
        }

    }
}
