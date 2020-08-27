using EmployeeManagement.Web.Models;
using EmployeeMangement.Web.Models;
using EmployeeMangement.Web.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<Department> GetDepartments()
        {
            return _context.Departments.ToList();
        }
        public Department GetDepartmentByID(int Did)
        {
            return _context.Departments.FirstOrDefault(x => x.Did == Did);
        }

        public void InsertDepartment(Department department)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();
        }

        public void UpdateDepartment(Department department)
        {
            _context.Entry(department).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteDepartment(int Eid)
        {
            Department department = _context.Departments.Find(Eid);
            _context.Remove(department);
            _context.SaveChanges();
        }
        public Department Reset()
        {
            return new Department { DepartmentName = "", Did = 0  };
        }
        
    }
}
