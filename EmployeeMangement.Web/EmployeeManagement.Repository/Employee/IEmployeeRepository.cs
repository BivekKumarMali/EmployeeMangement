using EmployeeManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangement.Web.Repository
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployees();
        Employee GetEmployeeByID(int Eid);
        void InsertEmployee(Employee employee);
        void DeleteEmployee(int Eid);
        void UpdateEmployee(Employee employee);

        Department GetDepartment(int Did);
        bool CheckDepartmentExist(int Did);
    }
}
