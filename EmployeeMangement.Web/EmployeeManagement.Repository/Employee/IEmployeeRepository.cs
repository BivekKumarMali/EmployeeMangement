using EmployeeManagement.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangement.Web.Repository
{
    public interface IEmployeeRepository
    {

        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<IEnumerable<Employee>> FilterEmployee(string departmentName);
        Employee GetEmployeeById(int? id);
        Task<Employee> GetEmployeeByEmail(string email);
        SelectList DepartmentListName();
        SelectList DepartmentListName(int id);
        SelectList DepartmentListId(int id);

        void AddEmployee(Employee Employee);
        void UpdateEmployee(Employee Employee);
        void DeleteEmployee(int id);


    }
}
