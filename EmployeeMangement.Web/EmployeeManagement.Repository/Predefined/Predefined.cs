using EmployeeManagement.Web.Models;
using EmployeeMangement.Web.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangement.Web.Repository
{
    public class Predefined : IPredefined
    {
        private readonly AppDbContext _context;
        private readonly IManager _manager;

        public Predefined(AppDbContext context, IManager manager)
        {
            _context = context;
            _manager = manager; 
        }


        public async Task<bool> CheckAdminRole()
        {
            return await _manager.GetRoleByName("Admin") != null;
        }
        
        public async Task<string> AddDefaulRole()
        {
            IdentityRole identityRole = new IdentityRole();
            if (!await CheckAdminRole())
            {
                identityRole = new IdentityRole { Name = "Admin" };
                await _manager.AddRoleManager(identityRole);
            }
            identityRole = await _manager.GetRoleByName("Admin");
            return identityRole.Id;
        }

        public async Task<bool> AddDefaulUser()
        {
            if(_manager.GetAllRoles().Count < 1)
            {
                Employee employee = _context.Employees.Find(1);
                employee.RoleId = await AddDefaulRole();
                employee.UserId = await _manager.AddUserManager(employee);
                _context.Employees.Update(employee);
                _context.SaveChanges();
            }
            return false;
        }
    }
}
