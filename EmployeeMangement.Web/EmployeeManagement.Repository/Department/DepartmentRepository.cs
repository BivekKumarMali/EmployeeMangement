using EmployeeManagement.Web.Models;
using EmployeeMangement.Web.Models;
using EmployeeMangement.Web.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace EmployeeMangement.Web.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private AppDbContext _context;

        public DepartmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            return await _context.Departments.ToListAsync();
        }
        public async Task<Department> GetDepartmentById(int Did)
        {
            return await _context.Departments.FindAsync(Did);
        }
 
        
        public async void AddDepartment(Department department)
        {
            await _context.Departments.AddAsync(department);
            SaveDepartment();
        } 
        public async void UpdateDepartment(Department department)
        {
            _context.Update(department);
            SaveDepartment();
        }
        public async void DeleteDepartment(int id)
        {
            Department department = await GetDepartmentById(id);
            _context.Departments.Remove(department);
            SaveDepartment();
        }




        public Department ResetDepartment()
        {
            return new Department { DepartmentName = " ", Did = 0 };
        }

        public async void SaveDepartment()
        {
            await _context.SaveChangesAsync();
        }

    }
}
