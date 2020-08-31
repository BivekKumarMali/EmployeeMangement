using EmployeeManagement.Web.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace EmployeeMangement.Web.Repository
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllDepartments();
        Department GetDepartmentById(int id);

        void AddDepartment(Department department);
        void UpdateDepartment(Department department);
        void DeleteDepartment(int id);

        Department ResetDepartment();
        void SaveDepartment();
    }
}
