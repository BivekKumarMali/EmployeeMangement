using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Web.Models;
using EmployeeManagement.Web.Repository;
using EmployeeManagement.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using System.Web.Http.Cors;

namespace EmployeeManagement.Web.EmployeeManagement.Core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Authorize(Roles = "Admin, HR")]
    public class DepartmentController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly DepartmentViewModel _departmentViewModel;
        private readonly IManager _manager;
        private readonly INotificationRepository _notificationRepository;

        public DepartmentController(
            AppDbContext context, 
            IDepartmentRepository departmentRepository, 
            INotificationRepository notificationRepository, 
            IManager manager
            )
        {
            _context = context;
            _departmentRepository = departmentRepository;
            _departmentViewModel = new DepartmentViewModel();
            _manager = manager;
            _notificationRepository = notificationRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await _departmentRepository.GetAllDepartments();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddandEdit(Department department)
        {
            department.DepartmentName = department.DepartmentName.Trim();
            if (department.Did == 0)
            {
                _departmentRepository.AddDepartment(department);
                _notificationRepository.AddDepartmentNotification();
                _notificationRepository.SendNotification("Added Department by Admin", null);
            }
            else
            {
                _departmentRepository.UpdateDepartment(department);
                _notificationRepository.EditDepartmentNotification();
                _notificationRepository.SendNotification("Edited Department by Admin", null);

            }
            return NoContent();
        }

        [HttpDelete("{did}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int did)
        {
            Department department = _departmentRepository.GetDepartmentById(did);

            _departmentRepository.DeleteDepartment(did);
            _notificationRepository.DeleteDepartmentNotification();

            _notificationRepository.SendNotification("Deleted Department by Admin", null);

            return RedirectToAction(nameof(Index));
        }
    }
}
