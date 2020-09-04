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
using Microsoft.AspNetCore.Identity;

namespace EmployeeMangement.Web.EmployeeManagement.Core.Controllers
{

    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly DepartmentViewModel _departmentViewModel;
        private readonly IManager _manager;

        public DepartmentController(AppDbContext context, IDepartmentRepository departmentRepository, IManager manager)
        {
            _context = context;
            _departmentRepository = departmentRepository;
            _departmentViewModel = new DepartmentViewModel();
            _manager = manager;
        }

        public async Task<IActionResult> Index()
        {
            _departmentViewModel.Departments = await _departmentRepository.GetAllDepartments();
            _departmentViewModel.Department = _departmentRepository.ResetDepartment();
            return View(_departmentViewModel);
        }

        public async Task<IActionResult> Add([Bind("Did,DepartmentName,RoleId")] Department department)
        {
            if (department.Did == 0)
            {                
                if (await _manager.AddRoleManager(department.DepartmentName))
                {
                    IdentityRole role = await _manager.GetRoleByName(department.DepartmentName);
                    department.RoleId = role.Id;
                    _departmentRepository.AddDepartment(department);
                }
            }
            else
            {
                if (await _manager.UpdateRoleManager(department.RoleId))
                {
                    _departmentRepository.UpdateDepartment(department);
                }
            }
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            Department department = _departmentRepository.GetDepartmentById(id);
            if (await _manager.DeleteRoleManager(department.RoleId))
            {
                _departmentRepository.DeleteDepartment(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.Did == id);
        }
    }
}
