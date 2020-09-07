using EmployeeManagement.Web.Models;
using EmployeeManagement.Web.ViewModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangement.Web.Repository
{
    public interface IManager
    {
        Task<string> AddUserManager(Employee employee);
        Task<bool> UpdateUserManager(Employee employee);
        Task<bool> DeleteUserManager(string userId);

        List<IdentityRole> GetAllRoles();
        Task<bool> AddRoleManager(IdentityRole rolesModel);
        Task<bool> UpdateRoleManager(IdentityRole rolesModel);
        Task<bool> DeleteRoleManager(string Id);

        Task<bool> AddUserRole(string roleId, string userId);
        Task<bool> UpdateUserRole(string roleId, string userId);
        Task<bool> DeleteUserRole(string roleId, string userId);
        Task<bool> UserRoleExist(string roleId, string userId);


        Task<IdentityRole> GetRoleByName(string departmentName);
        Task<IdentityRole> GetRoleById(string id);
        Task<IdentityUser> GetUserById(string id);
        Task<IdentityUser> GetUserByEmail(string email);


        Task<bool> UpdatePassword(PasswordResetView passwordResetView);

    }
}
