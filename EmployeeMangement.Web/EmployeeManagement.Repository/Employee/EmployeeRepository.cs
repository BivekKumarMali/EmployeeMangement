﻿using Dapper;
using EmployeeManagement.Web.Models;
using EmployeeMangement.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangement.Web.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private AppDbContext _context;
        private readonly UserManager<Roles> _userManager;

        public EmployeeRepository(AppDbContext context, UserManager<Roles> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            var joinDbContext = _context.Employees.Include(e => e.Department);
            return await joinDbContext.ToListAsync();
        }
        public Employee GetEmployeeById(int? Eid)
        {
            return _context.Employees.Find(Eid);
        }


        public async void AddEmployee(Employee Employee)
        {
            await _context.Employees.AddAsync(Employee);
            SaveEmployee();
        }

        public void UpdateEmployee(Employee Employee)
        {
            _context.Update(Employee);
            SaveEmployee();
        }
        public void DeleteEmployee(int id)
        {
            Employee Employee = GetEmployeeById(id);
            _context.Employees.Remove(Employee);
            SaveEmployee();
        }

        public SelectList DepartmentListName()
        {
            return new SelectList(_context.Departments, "Did", "DepartmentName");
        }
        public SelectList DepartmentListName(int id)
        {
            return new SelectList(_context.Departments, "Did", "DepartmentName", id);
        }
        public SelectList DepartmentListId(int id)
        {
            return new SelectList(_context.Departments, "Did", "Did", id);
        }

        public void SaveEmployee()
        {
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Employee>> FilterEmployee(string DepartmentName)
        {
            int Did = _context.Departments.FirstOrDefault(x => x.DepartmentName == DepartmentName).Did;
            var Join = _context.Employees.Include(e => e.Department);
            List<Employee> employees = await Join.ToListAsync();
            return employees.Where(x => x.Did == Did);
        }

        public async Task<Employee> GetEmployeeByEmail(string email)
        {
            if(await _context.Employees.AnyAsync(x => x.Email == email))
            {
                return await _context.Employees.FirstAsync(x => x.Email == email);
            }
            return null;
        }
    }
}
