using EmployeeManagement.Web.Models;
using EmployeeManagement.Web.ViewModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangement.Web.Repository
{
    public class Manager : IManager
    {
        private UserManager<Roles> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private IDepartmentRepository _departmentRepository;
        private IEmployeeRepository _employeeRepository;

        public Manager(
            UserManager<Roles> userManager,
            RoleManager<IdentityRole> roleManager,
            IDepartmentRepository departmentRepository,
            IEmployeeRepository employeeRepository
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _departmentRepository = departmentRepository;
            _employeeRepository = employeeRepository;
        }
        

        public async Task<bool> AddUserManager(Employee employee)
        {
            Department department = _departmentRepository.GetDepartmentById(employee.Did);
            Roles user = new Roles { UserName = employee.Email, Email = employee.Email, Role = department.DepartmentName.Trim() };
            var addUserManagerStatus = await _userManager.CreateAsync(user, employee.Password);
            Roles role = await GetUserByEmail(employee.Email);
            var addUserRole = await AddUserRole(department.RoleId, role.Id);
            return addUserManagerStatus.Succeeded;
        }

        public async Task<bool> AddUserRole(string roleId, string userId)
        {
            IdentityRole role = await GetRoleById(roleId);
            Roles user = await GetUserById(userId);
            var result = await _userManager.AddToRoleAsync(user, role.Name.Trim());
            return result.Succeeded;
        }

        

        public async Task<bool> DeleteUserManager(string userId)
        {
            Roles roles = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.DeleteAsync(roles);
            return result.Succeeded;
        }

        public async Task<bool> DeleteUserRole(string roleId, string userId)
        {
            IdentityRole role = await GetRoleById(roleId);
            Roles user = await GetUserById(userId);
            var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
            return result.Succeeded;
        }

        

        public async Task<bool> UpdateUserManager(string id, Employee employee)
        {
            Roles newUser = await _userManager.FindByIdAsync(id);
            newUser.Email = employee.Email;
            newUser.UserName = employee.Email;
            var updateUserManagerStatus = await _userManager.UpdateAsync(newUser);
            Employee pastEmployee = _employeeRepository.GetEmployeeById(employee.Eid);
            if (!(pastEmployee.Password == employee.Password))
            {
                var changePasswordStatus = await _userManager.ChangePasswordAsync(newUser, pastEmployee.Password, employee.Password);
                return updateUserManagerStatus.Succeeded && changePasswordStatus.Succeeded;
            }
            else
            {
                return updateUserManagerStatus.Succeeded;
            }
        }

        public async Task<bool> UpdateUserRole(string roleId, string userId)
        {
            if(!await UserRoleExist(roleId, userId))
            {
                List<Department> departments = await _departmentRepository.GetAllDepartments();
                foreach (var department in departments)
                {
                    if(await UserRoleExist(department.RoleId, userId))
                    {
                        await DeleteUserRole(department.RoleId, userId);
                    }
                }
                await AddUserRole(roleId, userId);
                return await UserRoleExist(roleId, userId);
            }            
            return true;
        }

        public async Task<IdentityRole> GetRoleById(string id)
        {
            return await _roleManager.FindByIdAsync(id);
        }

        public async Task<IdentityRole> GetRoleByName(string departmentName)
        {
            return await _roleManager.FindByNameAsync(departmentName);
        }

        public async Task<Roles> GetUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }
        
        public async Task<Roles> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        
        public async Task<bool> UserRoleExist(string roleId, string userId)
        {
            IdentityRole role = await GetRoleById(roleId);
            Roles user = await GetUserById(userId);
            return await _userManager.IsInRoleAsync(user, role.Name);
        }


        public async Task<bool> UpdatePassword(PasswordResetView passwordResetView, Employee employee)
        {
            Roles User = await _userManager.FindByEmailAsync(employee.Email);
            var updatePasswordStatus = await _userManager.ChangePasswordAsync(User, employee.Password, passwordResetView.NewPassword);
            return updatePasswordStatus.Succeeded;
        }

        public async Task<bool> AddRoleManager(Department department)
        {
            IdentityRole role = new IdentityRole { Name = department.DepartmentName };
            var addRoleStatus = await _roleManager.CreateAsync(role);
            return addRoleStatus.Succeeded;
        }

        public async Task<bool> UpdateRoleManager(Department department)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(department.RoleId);
            role.Name = department.DepartmentName;
            var updateRoleStatus = await _roleManager.UpdateAsync(role);
            return updateRoleStatus.Succeeded;

        }

        public async Task<bool> DeleteRoleManager(string roleId)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(roleId);
            var deleteRoleStatus = await _roleManager.DeleteAsync(role);
            return deleteRoleStatus.Succeeded;
        }
    }
}
