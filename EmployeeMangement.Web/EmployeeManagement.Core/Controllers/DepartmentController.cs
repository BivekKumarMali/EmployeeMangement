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
using EmployeeManagement.Web.ViewModel;

namespace EmployeeMangement.Web.EmployeeManagement.Core.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly DepartmentViewModel _departmentViewModel;

        public DepartmentController(AppDbContext context, IDepartmentRepository departmentRepository)
        {
            _context = context;
            _departmentRepository = departmentRepository;
            _departmentViewModel = new DepartmentViewModel();
        }

        // GET: Department
        public IActionResult Index()
        {
            _departmentViewModel.Departments = _departmentRepository.GetDepartments();
            if(_departmentViewModel.Department == null)
            _departmentViewModel.Department = _departmentRepository.Reset();
            ViewBag.Message = "working";
            return View(_departmentViewModel);
        }

        public void Reset()
        {
            _departmentRepository.Reset();
        }

        public IActionResult Add([Bind("Did,DepartmentName")] Department department)
        {
            if(department.Did == 0)
            {
                    Insert(department);
            }
            else
            {
                if (DepartmentExists(department.Did))
                {
                    Insert(department);
                }
            }
            return RedirectToAction(nameof(Index));
        }
        public void Insert(Department department)
        {
            _departmentRepository.InsertDepartment(department);
        }
        [HttpGet("Department/Edit/{id}")]
        public ActionResult Edit(int id)
        {
            _departmentViewModel.Department = _departmentRepository.GetDepartmentByID(id);

            return View(_departmentViewModel.Department);
        }

        // POST: Department/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Did,DepartmentName")] Department department)
        {
            if (id != department.Did)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Did))
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
            return View(department);
        }

        public IActionResult Delete(int id)
        {
            _departmentRepository.DeleteDepartment(id);
            return RedirectToAction(nameof(Index));
        }

        
        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.Did == id);
        }
    }
}
