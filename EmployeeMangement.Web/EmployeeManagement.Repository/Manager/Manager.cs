using EmployeeManagement.Web.Models;
using EmployeeManagement.Web.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Repository
{
    public class Manager : IManager
    {
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public Manager(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public List<IdentityUser> GetAlUsers()
        {
            return _userManager.Users.ToList();
        }
        public async Task<string> AddUserManager(Employee employee)
        {
            string email = GenerateEmail(employee.Name, employee.Surname);
            string password = "12345";
            IdentityUser identityUser = new IdentityUser { UserName = email, Email = email };
            var addUserManagerStatus = await _userManager.CreateAsync(identityUser, password);
            identityUser = await GetUserByEmail(email);
            await AddUserRole(employee.RoleId, identityUser.Id);
            return identityUser.Id;
        }

        private string GenerateEmail(string name, string surname)
        {
            string email = name + surname;
            email = email.ToLower();
            email = String.Concat(email.Where(c => !Char.IsWhiteSpace(c)));
            return email + "@gmail.com";
        }
        
        public async Task<bool> DeleteUserManager(string userId)
        {
            IdentityUser roles = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.DeleteAsync(roles);
            return result.Succeeded;
        }

        public async Task<bool> UpdateUserManager(Employee employee)
        {
            IdentityUser identityUser = await _userManager.FindByIdAsync(employee.UserId);
            identityUser.Email = GenerateEmail(employee.Name, employee.Surname);
            identityUser.UserName = identityUser.Email;
            var updateUserManagerStatus = await _userManager.UpdateAsync(identityUser);
            if (updateUserManagerStatus.Succeeded)
            {
                if(await UpdateUserRole(employee.RoleId, employee.UserId))
                {
                    return true;
                }
            }
            return updateUserManagerStatus.Succeeded;
        }

        public async Task<bool> AddUserRole(string roleId, string userId)
        {
            IdentityRole role = await GetRoleById(roleId);
            IdentityUser user = await GetUserById(userId);
            var result = await _userManager.AddToRoleAsync(user, role.Name);
            return result.Succeeded;
        }


        public async Task<bool> DeleteUserRole(string roleId, string userId)
        {
            IdentityRole role = await GetRoleById(roleId);
            IdentityUser user = await GetUserById(userId);
            var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
            return result.Succeeded;
        }

        public async Task<bool> UpdateUserRole(string roleId, string userId)
        {
            if(!await UserRoleExist(roleId, userId))
            {
                IdentityUser user = await GetUserById(userId);
                IList<string> allRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, allRoles);
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

        public async Task<IdentityUser> GetUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }
        
        public async Task<IdentityUser> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        
        public async Task<bool> UserRoleExist(string roleId, string userId)
        {
            IdentityRole role = await GetRoleById(roleId);
            IdentityUser identityUser = await GetUserById(userId);
            return await _userManager.IsInRoleAsync(identityUser, role.Name);
        }


        public async Task<bool> UpdatePassword(PasswordResetView passwordResetView)
        {
            IdentityUser identityUser = await _userManager.FindByEmailAsync(passwordResetView.Email);
            var token = _userManager.GeneratePasswordResetTokenAsync(identityUser).Result;
            var updatePasswordStatus = await _userManager.ResetPasswordAsync(identityUser, token, passwordResetView.NewPassword);
            return updatePasswordStatus.Succeeded;
        }

        public List<IdentityRole> GetAllRoles()
        {
            var list = _roleManager.Roles.ToList();
            return list;
        }
        public async Task<bool> AddRoleManager(IdentityRole identityRole)
        {
            IdentityRole ir = new IdentityRole { Name = identityRole.Name };
            var addRoleStatus = await _roleManager.CreateAsync(ir);
            return addRoleStatus.Succeeded;
        }

        public async Task<bool> UpdateRoleManager(IdentityRole identityRole)
        {
            IdentityRole ir =await _roleManager.FindByIdAsync(identityRole.Id);
            ir.Name = identityRole.Name;
            var updateRoleStatus = await _roleManager.UpdateAsync(ir);
            return updateRoleStatus.Succeeded;

        }

        public async Task<bool> DeleteRoleManager(string Id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(Id);
            var deleteRoleStatus = await _roleManager.DeleteAsync(role);
            return deleteRoleStatus.Succeeded;
        }

        public string GetUserID(ClaimsPrincipal claimsPrincipal)
        {
            IdentityUser user = _userManager.GetUserAsync(claimsPrincipal).Result;
            return user.Id;
        }
    }
}
