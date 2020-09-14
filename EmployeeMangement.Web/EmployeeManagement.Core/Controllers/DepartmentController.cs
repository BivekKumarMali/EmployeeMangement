using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Web.Models;
using EmployeeManagement.Web.Repository;
using EmployeeManagement.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace EmployeeManagement.Web.EmployeeManagement.Core.Controllers
{
    [Authorize(Roles = "Admin,HR")]
    public class DepartmentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly DepartmentViewModel _departmentViewModel;
        private readonly IManager _manager;
        private readonly INotificationRepository _notificationRepository;

        public DepartmentController(AppDbContext context, IDepartmentRepository departmentRepository, INotificationRepository notificationRepository, IManager manager)
        {
            _context = context;
            _departmentRepository = departmentRepository;
            _departmentViewModel = new DepartmentViewModel();
            _manager = manager;
            _notificationRepository = notificationRepository;
        }

        public async Task<IActionResult> Index()
        {
            _departmentViewModel.Departments = await _departmentRepository.GetAllDepartments();
            _departmentViewModel.Department = _departmentRepository.ResetDepartment();
            return View(_departmentViewModel);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Add([Bind("Did,DepartmentName,RoleId")] Department department)
        {
            department.DepartmentName = department.DepartmentName.Trim();
            if (department.Did == 0)
            {
                _departmentRepository.AddDepartment(department);
                _notificationRepository.AddDepartmentNotification();
            }
            else
            {
                _departmentRepository.UpdateDepartment(department);
                _notificationRepository.EditDepartmentNotification();


            }
            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            Department department = _departmentRepository.GetDepartmentById(id);

            _departmentRepository.DeleteDepartment(id);

            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.Did == id);
        }
    }
}
