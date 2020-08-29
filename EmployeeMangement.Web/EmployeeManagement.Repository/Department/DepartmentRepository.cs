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
        private AppDbContext _context;
        private int _size; 
        
        SqlConnection con;
        private Department departments;
        
        public DepartmentRepository(AppDbContext context)
        {
            _context = context;
        }
        
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
            _size = departments.Capacity;

            return departments;
            // return await _context.Departments.ToListAsync();
        }
        public Department GetDepartmentById(int Did)
        {
            return  _context.Departments.Find(Did);
        }
 
        
        public void AddDepartment(Department department)
        {
            OpenConnection();
            string query = "INSERT INTO Departments(DepartmentName) VALUES(@DepartmentName)";
            SqlCommand cmd = new SqlCommand(query, con);
            // Passing parameter values  
            _size += 1;
            cmd.Parameters.AddWithValue("@Did", _size);
            cmd.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
            cmd.ExecuteNonQuery();
            //await _context.Departments.AddAsync(department);
            //SaveDepartment();
        }
        public void UpdateDepartment(Department department)
        {
            OpenConnection();
            string query = "UPDATE Departments SET DepartmentName = " + department.DepartmentName + " WHERE Did = " + department.Did;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            CloseConnection();
         /*   _context.Update(department);
            SaveDepartment();
        */
        }
        public void DeleteDepartment(int id)
        {
            OpenConnection();
            string query = "DELETE FROM Departments WHERE Did =" + id;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            CloseConnection();
            /* Department department = GetDepartmentById(id);
             _context.Departments.Remove(department);
             SaveDepartment();*/
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

        public void SaveDepartment()
        {
             _context.SaveChanges();
        }

    }
}
