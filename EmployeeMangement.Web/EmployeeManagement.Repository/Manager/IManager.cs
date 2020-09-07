using EmployeeManagement.Web.Models;
using EmployeeManagement.Web.ViewModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangement.Web.Repository
{
    public interface IManager
    {
        Task<bool> AddUserManager(Employee employee);
        Task<bool> UpdateUserManager(string id, Employee employee);
        Task<bool> DeleteUserManager(string userId);


        Task<bool> AddRoleManager(Department department);
        Task<bool> UpdateRoleManager(Department department);
        Task<bool> DeleteRoleManager(string roleId);

        Task<bool> AddUserRole(string roleId, string userId);
        Task<bool> UpdateUserRole(string roleId, string userId);
        Task<bool> DeleteUserRole(string roleId, string userId);
        Task<bool> UserRoleExist(string roleId, string userId);


        Task<IdentityRole> GetRoleByName(string departmentName);
        Task<IdentityRole> GetRoleById(string id);
        Task<Roles> GetUserById(string id);
        Task<Roles> GetUserByEmail(string email);


        Task<bool> UpdatePassword(PasswordResetView passwordResetView, Employee employee);

    }
}
