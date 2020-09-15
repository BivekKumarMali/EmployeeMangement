using EmployeeManagement.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Models
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<IsRead> IsReads { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Department>().HasData(new Department { Did = 1, DepartmentName = "Default" });
            modelBuilder.Entity<Employee>().HasData(new Employee { Eid = 2, Did = 1, ContactNumber = 12, Address = "assd", Qualification = "yu", Name = "HR", Surname = "Default", RoleId = "", UserId = "" });;
            modelBuilder.Entity<Employee>().HasData(new Employee { Eid = 1, Did = 1, ContactNumber = 12, Address = "assd", Qualification = "yu", Name = "admin", Surname = "Default", RoleId = "", UserId = "" });;


        }
    }
}