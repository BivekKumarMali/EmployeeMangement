using Dapper;
using EmployeeManagement.Web.Models;
using EmployeeMangement.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangement.Web.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConfiguration _config;
        private List<Department> _listOfDepartments = new List<Department>();
        public IDbConnection connection

        {
            get { return new SqlConnection(_config.GetConnectionString("DefaultConnection")); }
        }

        public EmployeeRepository(IConfiguration config)
        {
            _config = config;
            IDbConnection con = connection;
            con.Open();
            var result = con.Query<Department>("Select * from Departments");
            _listOfDepartments = result.ToList();
            con.Close();
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            using (IDbConnection con = connection)
            {
                string Query = "Select * from Employees";
                con.Open();
                var result = await con.QueryAsync<Employee>(Query);
                IEnumerable<Employee> employees = result.ToList();
                foreach (Employee employee in employees)
                {
                    employee.Department = _listOfDepartments.Find(x => x.Did == employee.Did);
                }
                return employees;
            }
        }
        public Employee GetEmployeeById(int? Eid)
        {
            using (IDbConnection con = connection)
            {
                string sQuery = "select * from employees where Eid =" + Eid;
                con.Open();
                var result = con.Query<Employee>(sQuery);
                return result.FirstOrDefault();
            }
        }


        public void AddEmployee(Employee Employee)
        {
            using (IDbConnection con = connection)
            {
                string query = "INSERT INTO Employees VALUES(@Name,@Surname,@Address,@Qualification,@ContactNumber,@Did)";
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@Name", Employee.Name);
                parameters.Add("@Surname", Employee.Surname);
                parameters.Add("@Address", Employee.Address);
                parameters.Add("@Qualification", Employee.Qualification);
                parameters.Add("@ContactNumber", Employee.ContactNumber);
                parameters.Add("@Did", Employee.Did);
                con.Execute(query, parameters);
            }
        }
        public void UpdateEmployee(Employee Employee)
        {
            using (IDbConnection con = connection)
            {
                string query = "UPDATE Employees SET Name = '" + Employee.Name + "',Surname='" + Employee.Surname + "', Address ='" + Employee.Address + "',Qualification ='" + Employee.Qualification +
                "',Contact = " + Employee.ContactNumber + " ,Did = " + Employee.Did + " WHERE Eid = " + Employee.Eid;
                con.Execute(query);
            }
        }
        public void DeleteEmployee(int id)
        {
            using (IDbConnection con = connection)
            {
                string Query = "DELETE FROM Employees WHERE Eid =" + id;
                con.Open();
                con.Execute(Query);
            }
        }

        public SelectList DepartmentListName()
        {
            return new SelectList(_listOfDepartments, "Did", "DepartmentName");
        }
        public SelectList DepartmentListName(int id)
        {
            return new SelectList(_listOfDepartments, "Did", "DepartmentName", id);
        }
        public SelectList DepartmentListId(int id)
        {
            return new SelectList(_listOfDepartments, "Did", "Did", id);
        }
        


    }
}
