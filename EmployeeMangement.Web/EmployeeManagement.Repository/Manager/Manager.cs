using EmployeeManagement.Web.Models;
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

        public Manager(
            UserManager<Roles> userManager,
            RoleManager<IdentityRole> roleManager,
            IDepartmentRepository departmentRepository
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _departmentRepository = departmentRepository;
        }
        public async Task<bool> AddRoleManager(string departmentName)
        {
            IdentityRole role = new IdentityRole { Name = departmentName };
            var result = await _roleManager.CreateAsync(role);
            return result.Succeeded;
        }

        public async Task<bool> AddUserManager(string email, int did)
        {
            string role = _departmentRepository.GetDepartmentById(did).DepartmentName;
            Roles user = new Roles { UserName = email, Email = email, Role = role };
            var result = await _userManager.CreateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> AddUserRole(string roleId, string userId)
        {
            IdentityRole role = await GetRoleById(roleId);
            Roles user = await GetUserById(userId);
            var result = await _userManager.AddToRoleAsync(user, role.Name);
            return result.Succeeded;
        }

        public async Task<bool> DeleteRoleManager(string id)
        {
            IdentityRole role = await GetRoleById(id);
            var result = await _roleManager.DeleteAsync(role);
            return result.Succeeded;
        }

        public async Task<bool> DeleteUserManager(string email)
        {
            Roles roles = await _userManager.FindByEmailAsync(email);
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

        public async Task<bool> UpdateRoleManager(string id)
        {
            IdentityRole role= await GetRoleById(id);
            var result = await _roleManager.UpdateAsync(role);
            return result.Succeeded;
        }

        public async Task<bool> UpdateUserManager(string email)
        {
            Roles newUser = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.UpdateAsync(newUser);
            return result.Succeeded;
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
            }
            else
            {
                await AddUserRole(roleId, userId); 
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
        
        public async Task<bool> UserRoleExist(string roleId, string userId)
        {
            IdentityRole role = await GetRoleById(roleId);
            Roles user = await GetUserById(userId);
            return await _userManager.IsInRoleAsync(user, role.Name);
        }
    }
}
