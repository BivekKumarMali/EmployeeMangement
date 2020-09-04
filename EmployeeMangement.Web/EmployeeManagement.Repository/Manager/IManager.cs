using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangement.Web.Repository
{
    public interface IManager
    {
        Task<bool> AddUserManager(string email, int did);
        Task<bool> UpdateUserManager(string email);
        Task<bool> DeleteUserManager(string email);
        
        Task<bool> AddRoleManager(string departmentName);
        Task<bool> UpdateRoleManager(string id);
        Task<bool> DeleteRoleManager(string id);

        Task<bool> AddUserRole(string roleId, string userId);
        Task<bool> UpdateUserRole(string roleId, string userId);
        Task<bool> DeleteUserRole(string roleId, string userId);


        Task<IdentityRole> GetRoleByName(string departmentName);
        Task<IdentityRole> GetRoleById(string id);
        Task<IdentityUser> GetUserById(string id);
    }
}
