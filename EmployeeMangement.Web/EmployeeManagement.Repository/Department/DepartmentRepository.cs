using EmployeeManagement.Web.Models;
using EmployeeMangement.Web.Models;
using EmployeeMangement.Web.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeMangement.Web.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        
        SqlConnection con;
        
        public List<Department> GetAllDepartments()
        {
            List<Department> departments = new List<Department>();
            Department department;
            OpenConnection();
            SqlCommand cmd = new SqlCommand("select * from Departments", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                department = new Department();
                department.Did = Convert.ToInt32(reader[0]);
                department.DepartmentName = reader[1].ToString();
                departments.Add(department);
            }
            CloseConnection();

            return departments;
        }
 
        
        public void AddDepartment(Department department)
        {
            OpenConnection();
            string query = "INSERT INTO Departments(DepartmentName) VALUES(@DepartmentName)";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
            cmd.ExecuteNonQuery();
            CloseConnection();
        }
        public void UpdateDepartment(Department department)
        {
            OpenConnection();
            string query = "UPDATE Departments SET DepartmentName = " + department.DepartmentName + " WHERE Did = " + department.Did;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            CloseConnection();
        }
        public void DeleteDepartment(int id)
        {
            OpenConnection();
            string query = "DELETE FROM Departments WHERE Did =" + id;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            CloseConnection();
        }

        
        public void OpenConnection()
        {
            string cs = "Server=ELENA\\SQLEXPRESS;Database=EmployeeManagement;Trusted_Connection=True;";
            con = new SqlConnection(cs);
            con.Open();
        }
        public void CloseConnection() {
            con.Close();
        }

        public Department ResetDepartment()
        {
            return new Department { DepartmentName = " ", Did = 0 };
        }


    }
}
