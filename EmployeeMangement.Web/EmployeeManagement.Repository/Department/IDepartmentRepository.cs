using EmployeeManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangement.Web.Repository
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetDepartments();
        Department GetDepartmentByID(int Did);
        void InsertDepartment(Department Department);
        void DeleteDepartment(int Eid);
        void UpdateDepartment(Department Department);
        Department Reset();

    }
}
