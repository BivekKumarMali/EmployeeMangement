using Dapper;
using EmployeeManagement.Web.Models;
using EmployeeMangement.Web.Models;
using EmployeeMangement.Web.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
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

        public async Task<List<Department>> GetAllDepartments()
        {
            return await _context.Departments.ToListAsync();
        }
        public Department GetDepartmentById(int Did)
        {
            return _context.Departments.Find(Did);
        }


        public void AddDepartment(Department department)
        {
            _context.Departments.AddAsync(department);
            SaveDepartment();
        }
        public void UpdateDepartment(Department department)
        {
            _context.Update(department);
            SaveDepartment();
        }
        public void DeleteDepartment(int id)
        {

            Department department = GetDepartmentById(id);
            _context.Departments.Remove(department);
            SaveDepartment();
        }

        public Department ResetDepartment()
        {
            return new Department { DepartmentName = " ", Did = 0, RoleId= ""};
        }

        public void SaveDepartment()
        {
            _context.SaveChanges();
        }
    }
}
