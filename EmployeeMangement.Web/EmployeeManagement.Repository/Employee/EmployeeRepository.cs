using EmployeeManagement.Web.Models;
using EmployeeMangement.Web.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangement.Web.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private AppDbContext _context;
        private List<Department> _departments;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
            _departments = context.Departments.ToList();
        }


        public IEnumerable<Employee> GetEmployees()
        {
            List<Employee> employees = _context.Employees.ToList();
            foreach (Employee employee in employees){
                if (CheckDepartmentExist(employee.Did))
                {
                    employee.Department = GetDepartment(employee.Did);
                }
                else
                {
                    employees.Remove(employee);
                }
            }
            return employees;
        }


        public Employee GetEmployeeByID(int Eid)
        {
            Employee employee = _context.Employees.Find(Eid);
            employee.Department = GetDepartment(employee.Did);
            return employee;
        }

        public void DeleteEmployee(int Eid)
        {
            Employee employee = GetEmployeeByID(Eid);
            _context.Employees.Remove(employee);
            _context.SaveChanges();
        }

        public void InsertEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public void UpdateEmployee(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public Department GetDepartment(int Did)
        {
            return _departments.Find(x => x.Did == Did);
        }

        public bool CheckDepartmentExist(int Did)
        {
            return _departments.Exists(x => x.Did == Did);
        }

    }
}
