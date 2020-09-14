using EmployeeManagement.Web.Models;
using EmployeeManagement.Web.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Repository
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
        
        public async Task<string> AddDefaulAdminRole()
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

        public async Task<bool> AddDefaulAdminUser()
        {
            if(_manager.GetAlUsers().Count < 2)
            {
                Employee employee = _context.Employees.Find(1);
                employee.RoleId = await AddDefaulAdminRole();
                employee.UserId = await _manager.AddUserManager(employee);
                _context.Employees.Update(employee);
                _context.SaveChanges();
            }
            return false;
        }


        public async Task<bool> AddDefaulHRUser()
        {
            if (_manager.GetAlUsers().Count < 2)
            {
                Employee employee = _context.Employees.Find(2);
                employee.RoleId = await AddDefaulHRRole();
                employee.UserId = await _manager.AddUserManager(employee);
                _context.Employees.Update(employee);
                _context.SaveChanges();
            }
            return false;
        }

        private async Task<string> AddDefaulHRRole()
        {
            IdentityRole identityRole = new IdentityRole();
            if (!await CheckHRRole())
            {
                identityRole = new IdentityRole { Name = "HR" };
                await _manager.AddRoleManager(identityRole);
            }
            identityRole = await _manager.GetRoleByName("HR");
            return identityRole.Id;
        }

        private async Task<bool> CheckHRRole()
        {
            return await _manager.GetRoleByName("HR") != null;
        }
    }
}
