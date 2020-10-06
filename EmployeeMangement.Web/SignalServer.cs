using EmployeeManagement.Web.Models;
using EmployeeManagement.Web.Repository;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web
{
    public class SignalServer: Hub
    {
        private readonly AppDbContext _context;
        private readonly IEmployeeRepository _employeeRepository;

        public SignalServer(AppDbContext context, IEmployeeRepository employeeRepository)
        {
            _context = context;
            _employeeRepository = employeeRepository;
        }
        public override async Task OnConnectedAsync()
        {
            if (Context.User.IsInRole("Admin"))
            {
                await Groups.AddToGroupAsync(this.Context.ConnectionId, "Admin");
            }
            else if (Context.User.IsInRole("HR"))
            {
                await Groups.AddToGroupAsync(this.Context.ConnectionId, "HR");
            }
            else if (Context.User.IsInRole("Employee"))
            {
                Task<IEnumerable<Employee>> T = _employeeRepository.GetAllEmployees();
                var employees = await T;
                T.Wait();
                foreach (var employee in employees)
                {
                    string email = employee.Name + employee.Surname + "@gmail.com";
                    if(email.ToLower() == Context.User.Identity.Name)
                    {
                        var grpName = "Employee" + employee.Did;
                        await this.Groups.AddToGroupAsync(this.Context.ConnectionId, grpName);
                    }
                }

            }
            await base.OnConnectedAsync();
        }
    }
}
