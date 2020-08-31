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
        private readonly IConfiguration _config;
        public IDbConnection connection
        {
            get{ return new SqlConnection(_config.GetConnectionString("DefaultConnection")); }
        }

        public DepartmentRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<Department>> GetAllDepartments()
        {
            using (IDbConnection con = connection)
            {
                string Query = "Select * from Departments";
                con.Open();
                var result = await con.QueryAsync<Department>(Query);
                return result.ToList();
            }
        }


        public Department ResetDepartment()
        {
            return new Department { DepartmentName = " ", Did = 0 };
        }

        public void AddDepartment(Department department)
        {
            using (IDbConnection con = connection)
            {
                string query = "INSERT INTO Departments(DepartmentName) VALUES(@DepartmentName)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@DepartmentName", department.DepartmentName);
                con.Execute(query, parameters);
            }
        }

        public void UpdateDepartment(Department department)
        {
            using (IDbConnection con = connection)
            {
                string query = "UPDATE Departments SET DepartmentName = " + department.DepartmentName + " WHERE Did = " + department.Did;
                con.Execute(query);
            }
        }

        public void DeleteDepartment(int id)
        {
            using (IDbConnection con = connection)
            {
                string Query = "DELETE FROM Departments WHERE Did =" + id;
                con.Open();
                con.Execute(Query);
            }
        }
    }
}
