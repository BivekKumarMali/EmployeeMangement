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

namespace EmployeeMangement.Web.EmployeeManagement.Core.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(AppDbContext context, IEmployeeRepository employeeRepository)
        {
            _context = context;
            _employeeRepository = employeeRepository;
        }

        public IActionResult Index()
        {
            return View(_employeeRepository.GetAllEmployees());
        }


        public IActionResult Create()
        {
            ViewData["DepartmentName"] = _employeeRepository.DepartmentListName();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Eid,Name,Surname,Address,Qualification,ContactNumber,Did")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeRepository.AddEmployee(employee);
                return RedirectToAction(nameof(Index));
            }
            ViewData["Did"] = _employeeRepository.DepartmentListId(employee.Did);
            return View(employee);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Employee employee = _employeeRepository.GetEmployeeById(id);
            //var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["DepartmentName"] = _employeeRepository.DepartmentListName(employee.Did);
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Eid,Name,Surname,Address,Qualification,ContactNumber,Did")] Employee employee)
        {
            if (id != employee.Eid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _employeeRepository.UpdateEmployee(employee);
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

        public IActionResult Delete(int id)
        {
            _employeeRepository.DeleteEmployee(id);
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Eid == id);
        }
    }
}
