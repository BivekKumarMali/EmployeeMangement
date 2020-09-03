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
using Microsoft.AspNetCore.Authorization;

namespace EmployeeMangement.Web.EmployeeManagement.Core.Controllers
{

    [Authorize]
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

        public async Task<IActionResult> Index()
        {
            _departmentViewModel.Departments = await _departmentRepository.GetAllDepartments();
            _departmentViewModel.Department = _departmentRepository.ResetDepartment();
            return View(_departmentViewModel);
        }

        public IActionResult Add([Bind("Did,DepartmentName")] Department department)
        {
            if (department.Did == 0)
            {
                _departmentRepository.AddDepartment(department);
            }
            else
            {
                _departmentRepository.UpdateDepartment(department);
            }
            return RedirectToAction(nameof(Index));
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
