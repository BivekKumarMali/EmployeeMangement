using EmployeeManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.ViewModel
{
    public class EmployeeViewModel
    {
        public Employee Employee { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
