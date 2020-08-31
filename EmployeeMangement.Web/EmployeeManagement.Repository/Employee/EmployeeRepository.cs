using EmployeeManagement.Web.Models;
using EmployeeMangement.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangement.Web.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private List<Department> _listOfDepartments = new List<Department>();

        SqlConnection con;

        public EmployeeRepository()
        {
            Department department;
            OpenConnection();
            SqlCommand cmd = new SqlCommand("select * from Departments", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                department = new Department();
                department.Did = Convert.ToInt32(reader[0]);
                department.DepartmentName = reader[1].ToString();
                _listOfDepartments.Add(department);
            }
            CloseConnection();
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            Employee employee = new Employee();
            OpenConnection();
            SqlCommand cmd = new SqlCommand("select * from employees", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                employee = new Employee();
                employee.Eid = Convert.ToInt32(reader[0]);
                employee.Name = reader[1].ToString();
                employee.Surname = reader[2].ToString();
                employee.Address = reader[3].ToString();
                employee.Qualification = reader[4].ToString();
                employee.ContactNumber = Convert.ToInt64(reader[5]);
                employee.Did = Convert.ToInt32(reader[6]);
                employees.Add(employee);
            }
            CloseConnection();
            foreach (Employee employeeForLoop  in employees){
                employeeForLoop.Department = _listOfDepartments.Find(x => x.Did == employee.Did);
            }
            return employees;
        }
        public Employee GetEmployeeById(int? Eid)
        {
            Employee employee = new Employee();
            OpenConnection();
            SqlCommand cmd = new SqlCommand("select * from employees where Eid ="+Eid, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                employee = new Employee();
                employee.Eid = Convert.ToInt32(reader[0]);
                employee.Name = reader[1].ToString();
                employee.Surname = reader[2].ToString();
                employee.Address = reader[3].ToString();
                employee.Qualification = reader[4].ToString();
                employee.ContactNumber = Convert.ToInt64(reader[5]);
                employee.Did = Convert.ToInt32(reader[6]);
            }
            CloseConnection();
            return employee;
        }


        public void AddEmployee(Employee Employee)
        {
            OpenConnection();
            string query = "INSERT INTO Employees VALUES(@Name,@Surname,@Address,@Qualification,@ContactNumber,@Did)";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@Name", Employee.Name);
            cmd.Parameters.AddWithValue("@Surname", Employee.Surname);
            cmd.Parameters.AddWithValue("@Address", Employee.Address);
            cmd.Parameters.AddWithValue("@Qualification", Employee.Qualification);
            cmd.Parameters.AddWithValue("@ContactNumber", Employee.ContactNumber);
            cmd.Parameters.AddWithValue("@Did", Employee.Did);
            cmd.ExecuteNonQuery();
        }
        public void UpdateEmployee(Employee Employee)
        {
            OpenConnection();
            string query = "UPDATE Employees SET Name = '" + Employee.Name + "',Surname='" +Employee.Surname +"', Address ='"+Employee.Address + "',Qualification ='"+Employee.Qualification +
                "',Contact = "+Employee.ContactNumber + " ,Did = "+ Employee.Did+ " WHERE Eid = " + Employee.Eid;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            CloseConnection();
        }
        public void DeleteEmployee(int id)
        {
            OpenConnection();
            string query = "DELETE FROM Employees WHERE Eid =" + id;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            CloseConnection();
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
        public void OpenConnection()
        {
            string cs = "Server=ELENA\\SQLEXPRESS;Database=EmployeeManagement;Trusted_Connection=True;";
            con = new SqlConnection(cs);
            con.Open();
        }
        public void CloseConnection()
        {
            con.Close();
        }


    }
}
